import { Card, Flex, Text } from "@radix-ui/themes";
import { ChartCardProps } from "./ChartCard.types";

import classNames from "./ChartCard.module.css";

export const ChartCard = ({
  title,
  width = "100%",
  height = "380px",
  headerControl,
  children,
}: ChartCardProps) => {
  return (
    <Card
      className={classNames.root}
      variant="surface"
      style={{ width, height }}
    >
      <Flex justify="between" align="center">
        <Text size="3" weight="bold">
          {title}
        </Text>
        <Flex gap="2">{headerControl}</Flex>
      </Flex>
      <Flex className={classNames.container} direction="column">
        {children}
      </Flex>
    </Card>
  );
};
