import { Flex, Text } from "@radix-ui/themes";
import {
  YAxis,
  XAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  BarChart,
  Bar,
  LineChart,
  Line,
} from "recharts";
import { ChartCard } from "../ChartCard";
import { ActivityChartProps } from "./ActivityChart.types";

const COLORS = ["#eb5757", "#f8b602"];

const activityData = [
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

const completedOrderData = [
  {
    name: "Jan",
    pv: 100,
  },
  {
    name: "Feb",
    pv: 200,
  },
  {
    name: "Mar",
    pv: 300,
  },
  {
    name: "Apr",
    pv: 150,
  },
  {
    name: "May",
    pv: 200,
  },
  {
    name: "Jun",
    pv: 500,
  },
  {
    name: "Jul",
    pv: 222,
  },
  {
    name: "Aug",
    pv: 550,
  },
  {
    name: "Sep",
    pv: 400,
  },
  {
    name: "Oct",
    pv: 881,
  },
  {
    name: "Nov",
    pv: 1000,
  },
  {
    name: "Dec",
    pv: 1000,
  },
];

const deliveredOrderData = [
  {
    name: "Jan",
    pv: 70,
  },
  {
    name: "Feb",
    pv: 180,
  },
  {
    name: "Mar",
    pv: 240,
  },
  {
    name: "Apr",
    pv: 148,
  },
  {
    name: "May",
    pv: 180,
  },
  {
    name: "Jun",
    pv: 110,
  },
  {
    name: "Jul",
    pv: 285,
  },
  {
    name: "Aug",
    pv: 350,
  },
  {
    name: "Sep",
    pv: 110,
  },
  {
    name: "Oct",
    pv: 150,
  },
  {
    name: "Nov",
    pv: 240,
  },
  {
    name: "Dec",
    pv: 300,
  },
];

export const ActivityChart = ({}: ActivityChartProps) => {
  return (
    <ChartCard title="Activity">
      <Flex direction="row" height="100%" gap="5">
        <ResponsiveContainer width="100%">
          <BarChart width={500} data={activityData}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <Bar dataKey="pv" fill={COLORS[1]} />
            <Bar dataKey="uv" fill={COLORS[0]} />
          </BarChart>
        </ResponsiveContainer>
        <Flex direction="column" maxWidth="400px" width="30%">
          <Flex direction="column" height="50%">
            <Text size="1" color="gray">
              Completed Order
            </Text>
            <Text size="3" weight="bold">
              {completedOrderData.reduce((acc, curr) => acc + curr.pv, 0)} Task
            </Text>
            <ResponsiveContainer>
              <LineChart data={completedOrderData}>
                <Line
                  type="monotone"
                  dataKey="pv"
                  stroke={COLORS[1]}
                  strokeWidth={3}
                  dot={false}
                />
              </LineChart>
            </ResponsiveContainer>
          </Flex>
          <Flex direction="column" height="50%">
            <Text size="1" color="gray">
              Order Delivered
            </Text>
            <Text size="3" weight="bold">
              {deliveredOrderData.reduce((acc, curr) => acc + curr.pv, 0)}
            </Text>
            <ResponsiveContainer>
              <LineChart data={deliveredOrderData}>
                <Line
                  type="monotone"
                  dataKey="pv"
                  stroke={COLORS[0]}
                  strokeWidth={3}
                  dot={false}
                />
              </LineChart>
            </ResponsiveContainer>
          </Flex>
        </Flex>
      </Flex>
    </ChartCard>
  );
};
