import { useEffect, useState } from "react";
import {
  Box,
  Button,
  Callout,
  Flex,
  Heading,
  Link,
  TextField,
} from "@radix-ui/themes";
import { useLocation, useNavigate } from "react-router";
import { InfoCircledIcon } from "@radix-ui/react-icons";
import { User } from "../../models";
import api from "../../api";

import classNames from "./SignIn.module.css";

const USERNAME = "john.admin@techfood.com";
const PASSWORD = "123456";

export const SignIn = () => {
  const [{ username, password, error }, setState] = useState({
    username: USERNAME,
    password: PASSWORD,
    error: "",
  });

  const backSrc = new URL(`../../assets/background.png`, import.meta.url).href;

  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  useEffect(() => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
  }, []);

  const handleSignIn = () => {
    if (username && password) {
      api
        .post<{
          accessToken: string;
          user: User;
        }>(
          "/v1/auth/signin",
          {
            username,
            password,
          },
          { silent: true }
        )
        .then(({ data }) => {
          localStorage.setItem("token", data.accessToken);
          localStorage.setItem("user", JSON.stringify(data.user));
          navigate(from, { replace: true });
        })
        .catch(({ response: { data } }) => {
          setState((prev) => ({ ...prev, error: data.message }));
        });
    }
  };

  return (
    <Flex
      className={classNames.root}
      direction="column"
      align="end"
      justify="center"
      style={{ backgroundImage: `url(${backSrc})` }}
    >
      <Box className={classNames.overlay} />
      <Flex
        className={classNames.container}
        direction="column"
        justify="center"
        gap="6"
      >
        <Flex mb="2" justify="center">
          <Heading size="8">TechFood</Heading>
        </Flex>
        <Box>
          <TextField.Root
            placeholder="Username"
            size="3"
            value={username}
            type="text"
            onChange={(e) =>
              setState((prev) => ({
                ...prev,
                username: e.target.value,
                error: "",
              }))
            }
          />
        </Box>
        <Box>
          <TextField.Root
            placeholder="Password"
            size="3"
            value={password}
            type="password"
            onChange={(e) =>
              setState((prev) => ({
                ...prev,
                password: e.target.value,
                error: "",
              }))
            }
          />
          <Flex gap="2" justify="end" mt="2">
            <Link href="#">Forgot password?</Link>
          </Flex>
        </Box>
        <Button
          size="4"
          onClick={handleSignIn}
          disabled={!username || !password}
        >
          Sign In
        </Button>
        {error && (
          <Callout.Root color="red" highContrast size="2" mt="4">
            <Callout.Icon>
              <InfoCircledIcon />
            </Callout.Icon>
            <Callout.Text>{error}</Callout.Text>
          </Callout.Root>
        )}
      </Flex>
    </Flex>
  );
};
