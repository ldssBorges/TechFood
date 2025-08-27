import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { Flex, Heading, Text } from "@radix-ui/themes";
import { useOrder } from "../../contexts";
import { t } from "../../i18n";

import classNames from "./ConfirmationPage.module.css";
import axios from "axios";

const timeout = 20000; // 20 seconds

export const ConfirmationPage = () => {
  const [number, setNumber] = useState<string | undefined>();
  const navigate = useNavigate();
  const { id } = useOrder();

  useEffect(() => {
    setTimeout(() => {
      navigate("/", { replace: true });
    }, timeout);

    const fetchNumber = async () => {
      const response = await axios.get<string>(
        `/api/v1/Preparations/${id}/number`
      );
      console.log(response);
      setNumber(response.data);
    };

    fetchNumber();
  }, [number, navigate]);

  return (
    <Flex
      className={classNames.root}
      direction="column"
      align="center"
      justify="center"
      gap="5"
    >
      <Flex
        className={classNames.header}
        align="center"
        justify="between"
        gap="5"
      >
        <Heading className={classNames.title}>
          {t("confirmationPage.title")}
        </Heading>
      </Flex>
      <Flex
        className={classNames.content}
        direction="column"
        gap="6"
        align="center"
      >
        <Text size="2" weight="medium" color="gray">
          {t("confirmationPage.subtitle")}
        </Text>
        <Flex
          className={classNames.receipt}
          direction="column"
          gap="2"
          align="center"
          justify="center"
        >
          <Text size="2" weight="medium">
            {t("confirmationPage.yourOrder")}
          </Text>
          <Flex gap="2" align="center" justify="center">
            <Text
              size="2"
              weight="medium"
              style={{ borderBottom: "3px solid var(--accent-10)" }}
            >
              {t("confirmationPage.orderNumber")}
            </Text>
            <Text size="5" weight="medium">
              {number}
            </Text>
          </Flex>
        </Flex>
      </Flex>
    </Flex>
  );
};
