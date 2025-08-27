import { Button, Flex, Separator, Text } from "@radix-ui/themes";
import { TrendingUpIcon, TrendingDownIcon, ArrowRightIcon } from "lucide-react";
import { ChartCard } from "../ChartCard";

const totalIncome = "12,890,00";
const income = "11,890,00";
const incomePercent = "+12%";
const expense = "1,000,00";
const expensePercent = "-10%";

export const WithdrawChart = () => {
  return (
    <ChartCard height="120px">
      <Flex direction="column" gap="4px">
        <Flex align="center" gap="30px">
          <Flex direction="column" gap="2px" flexGrow="1">
            <Text size="1" weight="medium" color="gray">
              Total Income
            </Text>
            <Text size="7" weight="bold">
              ${totalIncome}
            </Text>
          </Flex>
          <Separator orientation="vertical" size="3" />
          <Flex gap="30px">
            <Flex direction="column" gap="4px">
              <Text size="1" weight="medium" color="gray">
                Income
              </Text>
              <Text size="5" weight="bold">
                ${income}
              </Text>
              <Flex gap="6px">
                <TrendingUpIcon color="var(--grass-a11)" size="20" />
                <Text size="2" color="grass" weight="medium">
                  {incomePercent}
                </Text>
              </Flex>
            </Flex>
            <Flex direction="column" gap="4px">
              <Text size="1" weight="medium" color="gray">
                Expense
              </Text>
              <Text size="5" weight="bold">
                ${expense}
              </Text>
              <Flex gap="6px">
                <TrendingDownIcon color="var(--tomato-a11)" size="20" />
                <Text size="2" color="tomato" weight="medium">
                  {expensePercent}
                </Text>
              </Flex>
            </Flex>
          </Flex>
          <Button size="4">
            Withdraw
            <ArrowRightIcon />
          </Button>
        </Flex>
      </Flex>
    </ChartCard>
  );
};
