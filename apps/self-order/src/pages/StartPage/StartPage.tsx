import { useEffect, useState } from "react";
import {
  AlertDialog,
  Box,
  Button,
  Flex,
  IconButton,
  TextField,
} from "@radix-ui/themes";
import { ArrowRightIcon } from "lucide-react";
import { useNavigate } from "react-router";
import { t } from "../../i18n";
import axios, { isAxiosError } from "axios";
import { LanguageSwitch, CustomDialog } from "../../components";
import { validateCPF } from "../../utilities";
import { useCustomer, useOrder } from "../../contexts";
import { Customer } from "../../models";

import classNames from "./StartPage.module.css";

export const StartPage = () => {
  const [errorMessage, setErrorMessage] = useState("");
  const [documentNumber, setDocumentNumber] = useState("");
  const [showRegisterModal, setRegisterModal] = useState(false);

  const navigate = useNavigate();

  const { clearOrder } = useOrder();
  const { setCustomer } = useCustomer();

  useEffect(() => {
    clearOrder();
    setCustomer(null);
  }, [clearOrder, setCustomer]);

  const handleRegister = () => {
    if (validateCPF(documentNumber)) {
      navigate("/register/" + documentNumber);
      return;
    }
    setErrorMessage(t("startPage.invalidDocument"));
  };

  const handleIdentify = async () => {
    if (!documentNumber) return;

    try {
      const { status, data } = await axios.get<Customer>(
        `/api/v1/customers/${documentNumber}`
      );

      if (status === 200) {
        setCustomer(data);
        navigate("/menu");
      }
    } catch (error) {
      if (!isAxiosError(error)) {
        console.error("Erro inesperado:", error);
        return;
      }

      const status = error.response?.status;

      switch (status) {
        case 404:
          setRegisterModal(true);
          break;
        case 400:
          handleRegister();
          break;
        default:
          console.error("Erro inesperado:", error);
      }
    }
  };

  const handlerDontIdentify = () => {
    navigate("/menu");
  };

  const closeModal = () => {
    setRegisterModal(false);
  };

  const confirmRegister = () => {
    closeModal();
    handleRegister();
  };

  return (
    <Flex className={classNames.root} direction="column">
      <Flex className={classNames.languageSwitch} justify="end">
        <LanguageSwitch />
      </Flex>

      <Flex direction="column" align="center" justify="center" flexGrow="1">
        <Flex direction="column" gap="4">
          <Flex gap="2" align="center">
            <Box maxWidth="400px">
              <TextField.Root
                placeholder={t("startPage.documentNumber")}
                size="3"
                maxLength={11}
                onChange={(e) => setDocumentNumber(e.target.value)}
              />
            </Box>
            <IconButton
              size="3"
              disabled={documentNumber.length < 11}
              onClick={handleIdentify}
            >
              <ArrowRightIcon size="40" />
            </IconButton>
          </Flex>

          <Button onClick={handlerDontIdentify} variant="outline" size="3">
            {t("startPage.dontIdentify")}
          </Button>
        </Flex>
      </Flex>

      {errorMessage && (
        <AlertDialog.Root open={true}>
          <AlertDialog.Content>
            <AlertDialog.Description mb="5">
              <Flex justify="center">{errorMessage}</Flex>
            </AlertDialog.Description>
            <Flex gap="3" justify="center">
              <AlertDialog.Cancel onClick={() => setErrorMessage("")}>
                <Button variant="soft" color="gray">
                  {t("labels.close")}
                </Button>
              </AlertDialog.Cancel>
            </Flex>
          </AlertDialog.Content>
        </AlertDialog.Root>
      )}

      <CustomDialog
        dialogOpen={showRegisterModal}
        setDialogOpen={setRegisterModal}
        textBotton1="startPage.yes"
        textBotton2="startPage.no"
        title="Cliente não cadastrado"
        description="Identificamos que você não está cadastrado. Gostaria de se cadastrar?"
        onConfirm={confirmRegister}
        onCancel={closeModal}
      />
    </Flex>
  );
};
