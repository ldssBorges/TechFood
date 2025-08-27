import { Flex } from "@radix-ui/themes";
import {
  ActivityChart,
  OrderRateChart,
  PerformanceChart,
  PopularFoodChart,
  DailyActivityChart,
  WithdrawChart,
  CountChart,
} from "../../components";

import classNames from "./Dashboard.module.css";

const gap = "5" as any;

export const Dashboard = () => {
  return (
    <Flex className={classNames.root} direction="column" gap={gap}>
      <Flex direction="row" gap={gap}>
        <Flex direction="column" gap={gap} flexGrow="1">
          <WithdrawChart />
          <Flex direction="row" gap={gap}>
            <DailyActivityChart />
            <PerformanceChart />
          </Flex>
        </Flex>
        <Flex direction="column" gap={gap}>
          <CountChart />
        </Flex>
      </Flex>
      <Flex direction="row" gap={gap}>
        <Flex flexGrow="1" gap={gap}>
          <OrderRateChart />
        </Flex>
        <PopularFoodChart />
      </Flex>
      <Flex direction="row" gap={gap}>
        <ActivityChart />
      </Flex>
    </Flex>
  );
};
