import { useEffect, useState } from "react";
import { Box, Button, Flex, IconButton, TextField } from "@radix-ui/themes";
import * as Label from "@radix-ui/react-label";
import { Form, useNavigate, useParams } from "react-router";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { ArrowLeftIcon } from "lucide-react";
import { z } from "zod";
import axios from "axios";
import { useCustomer } from "../../contexts";

import classNames from "./RegisterPage.module.css";

const createCustomerSchema = z.object({
  name: z.string().min(1, "O campo nome é obrigatório"),
  email: z.string().email("Formato de email inválido"),
});

type CreateCustomerSchema = z.infer<typeof createCustomerSchema>;

export const RegisterPage = () => {
  const [documentNumber, setDocumentNumber] = useState("");
  const navigate = useNavigate();
  const params = useParams();
  const { setCustomer } = useCustomer();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateCustomerSchema>({
    resolver: zodResolver(createCustomerSchema),
  });

  const onSubmit = async ({ name, email }: CreateCustomerSchema) => {
    try {
      const { data } = await axios.post<{
        id: string;
      }>("/api/v1/customers", {
        cpf: params.doc,
        name,
        email,
      });

      setCustomer({
        id: data.id,
        name,
        email,
        documentType: "CPF",
        documentNumber: params.doc!,
      });

      navigate("/menu");
    } catch (error) {
      console.log("erro!!!");
      console.log(error);
    }
  };

  useEffect(() => {
    setDocumentNumber(params.doc!);
  }, [params.doc]);

  return (
    <Flex
      direction="column"
      align="center"
      justify="center"
      className={classNames.spaceup}
    >
      <Flex gap="2" align="center">
        <Box maxWidth="500px">
          <Form onSubmit={handleSubmit(onSubmit)}>
            <Label.Root className="label" htmlFor="cpf">
              CPF:
            </Label.Root>
            <TextField.Root
              name="cpf"
              size="3"
              maxLength={11}
              value={documentNumber}
              disabled={true}
            />
            <Label.Root className="label" htmlFor="nome">
              Nome:
            </Label.Root>
            <TextField.Root
              {...register("name", { required: "O campo nome é obrigatório" })}
              size="3"
              maxLength={255}
            />
            {errors.name && <span>{errors.name.message}</span>}
            <Label.Root className="label" htmlFor="email">
              Email:
            </Label.Root>
            <TextField.Root {...register("email")} size="3" maxLength={255} />
            {errors.email && <span>{errors.email.message}</span>}
            <Flex direction="row" justify="between">
              <IconButton
                type="button"
                onClick={() => {
                  navigate("/start");
                }}
                size="3"
              >
                <ArrowLeftIcon size="40" />
              </IconButton>
              <Button type="submit" size="3">
                Cadastrar
              </Button>
            </Flex>
          </Form>
        </Box>
      </Flex>
    </Flex>
  );
};
