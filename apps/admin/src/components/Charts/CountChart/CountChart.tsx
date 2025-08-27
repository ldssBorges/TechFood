import { Flex, IconButton, Text } from "@radix-ui/themes";
import { ChartCard } from "../ChartCard";
import {
  CircleAlertIcon,
  CircleCheckIcon,
  ClockIcon,
  ListCheckIcon,
} from "lucide-react";

const CountItem = ({ title, value, icon }: any) => {
  return (
    <Flex gap="4" align="center">
      <IconButton variant="outline" color="yellow" size="3">
        {icon}
      </IconButton>
      <Flex direction="column">
        <Text size="1" color="gray">
          {title}
        </Text>
        <Text size="6" weight="bold">
          {value}
        </Text>
      </Flex>
    </Flex>
  );
};

export const CountChart = () => {
  return (
    <ChartCard width="300px" height="345px">
      <Flex direction="column" justify="between" height="100%">
        <CountItem
          title="Total Order Complete"
          value="2.678"
          icon={<ListCheckIcon />}
        />
        <CountItem
          title="Total Order Delivered"
          value="1.236"
          icon={<CircleCheckIcon />}
        />
        <CountItem
          title="Total Order Canceled"
          value="23"
          icon={<CircleAlertIcon />}
        />
        <CountItem title="Order Pending" value="123" icon={<ClockIcon />} />
      </Flex>
    </ChartCard>
  );
};
