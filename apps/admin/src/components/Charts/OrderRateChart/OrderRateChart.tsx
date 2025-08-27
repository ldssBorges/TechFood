import { Box, Flex, Progress, Select, Text } from "@radix-ui/themes";
import {
  YAxis,
  AreaChart,
  XAxis,
  CartesianGrid,
  Tooltip,
  Area,
  ResponsiveContainer,
} from "recharts";
import { UserRoundIcon } from "lucide-react";
import { ChartCard } from "../ChartCard";
import { OrderRateChartProps } from "./OrderRateChart.types";

const COLORS = ["#eb5757", "#f8b602"];

const data = [
  {
    name: "Jan",
    uv: 4000,
    pv: 2400,
    amt: 2400,
  },
  {
    name: "Feb",
    uv: 3000,
    pv: 1398,
    amt: 2210,
  },
  {
    name: "Mar",
    uv: 2000,
    pv: 9800,
    amt: 2290,
  },
  {
    name: "Apr",
    uv: 2780,
    pv: 3908,
    amt: 2000,
  },
  {
    name: "May",
    uv: 1890,
    pv: 4800,
    amt: 2181,
  },
  {
    name: "Jun",
    uv: 2390,
    pv: 3800,
    amt: 2500,
  },
  {
    name: "Jul",
    uv: 3490,
    pv: 4300,
    amt: 2100,
  },
  {
    name: "Aug",
    uv: 3490,
    pv: 4300,
    amt: 2100,
  },
  {
    name: "Sep",
    uv: 3490,
    pv: 4300,
    amt: 2100,
  },
  {
    name: "Oct",
    uv: 3490,
    pv: 4300,
    amt: 2100,
  },
  {
    name: "Nov",
    uv: 3490,
    pv: 4300,
    amt: 2100,
  },
  {
    name: "Dec",
    uv: 3490,
    pv: 4300,
    amt: 2100,
  },
];

export const OrderRateChart = ({}: OrderRateChartProps) => {
  return (
    <ChartCard title="Order Rate">
      <Flex gap="2" justify="between">
        <Flex gap="5">
          <Flex gap="2" align="center">
            <UserRoundIcon size={30} />
            <Flex direction="column">
              <Text size="2" weight="medium" color="gray">
                Order Total
              </Text>
              <Text size="3" weight="bold">
                2,345
              </Text>
            </Flex>
          </Flex>
          <Flex direction="column" gap="2">
            <Flex justify="between">
              <Text size="2" weight="medium" color="gray">
                Target
              </Text>
              <Text size="3" weight="bold">
                9.985
              </Text>
            </Flex>
            <Box width="130px">
              <Progress size="2" value={70} variant="soft" radius="none" />
            </Box>
          </Flex>
        </Flex>
        <Flex gap="2">
          <Flex direction="column" gap="2"></Flex>
          <Flex direction="column" gap="2">
            <Select.Root defaultValue="month" size="2">
              <Select.Trigger />
              <Select.Content>
                <Select.Item value="month" textValue="Month">
                  Month
                </Select.Item>
                <Select.Item value="week" textValue="Week">
                  Week
                </Select.Item>
              </Select.Content>
            </Select.Root>
          </Flex>
        </Flex>
      </Flex>
      <ResponsiveContainer>
        <AreaChart data={data} margin={{ top: 20 }} barSize={10}>
          <defs>
            <linearGradient id="colorUv" x1="0" y1="0" x2="0" y2="1">
              <stop offset="5%" stopColor={COLORS[0]} stopOpacity={0.8} />
              <stop offset="95%" stopColor={COLORS[0]} stopOpacity={0} />
            </linearGradient>
            <linearGradient id="colorPv" x1="0" y1="0" x2="0" y2="1">
              <stop offset="5%" stopColor={COLORS[1]} stopOpacity={0.8} />
              <stop offset="95%" stopColor={COLORS[1]} stopOpacity={0} />
            </linearGradient>
          </defs>
          <XAxis dataKey="name" />
          <YAxis />
          <CartesianGrid strokeDasharray="3 3" />
          <Tooltip />
          <Area
            type="monotone"
            dataKey="uv"
            stroke={COLORS[0]}
            strokeWidth={3}
            fillOpacity={1}
            fill="url(#colorUv)"
          />
          <Area
            type="monotone"
            dataKey="pv"
            stroke={COLORS[1]}
            strokeWidth={3}
            fillOpacity={1}
            fill="url(#colorPv)"
          />
        </AreaChart>
      </ResponsiveContainer>
    </ChartCard>
  );
};
