import { Button, Flex, Heading, Text } from "@radix-ui/themes";
import { useNavigate } from "react-router";
import { t } from "../../i18n";

import classNames from "./Forbidden.module.css";

export const Forbidden = () => {
  const imgSrc = new URL(`../../assets/forbidden.png`, import.meta.url).href;

  const navigate = useNavigate();

  return (
    <Flex
      className={classNames.root}
      direction="column"
      align="center"
      justify="center"
      gap="4"
      p="2"
    >
      <img
        src={imgSrc}
        alt="Forbidden"
        className={classNames.image}
        height={300}
      />
      <Heading size="8" color="amber" mb="4">
        {t("forbidden.title")}
      </Heading>
      <Text size="4" color="gray">
        {t("forbidden.message")}
      </Text>
      <Text size="4" color="gray">
        {t("forbidden.contactMessage")}
      </Text>
      <Flex direction="column" mt="4">
        <Button size="4" onClick={() => navigate("/")}>
          {t("forbidden.backToHome")}
        </Button>
      </Flex>
    </Flex>
  );
};
