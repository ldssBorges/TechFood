import { Box, Flex, Text } from "@radix-ui/themes";
import { PieChart, Pie, Cell, ResponsiveContainer } from "recharts";
import { ChartCard } from "../ChartCard";
import { PopularFoodChartProps } from "./PopularFoodChart.types";

const COLORS = ["#eb5757", "#f8b602", "#a6c44a"];

const data = [
  { name: "Asian Food", value: 763 },
  { name: "Fast Food", value: 321 },
  { name: "Western Food", value: 69 },
];

export const PopularFoodChart = ({}: PopularFoodChartProps) => {
  return (
    <ChartCard title="Popular Food" width="300px">
      <ResponsiveContainer>
        <PieChart>
          <Pie
            data={data}
            cx="50%"
            cy="50%"
            innerRadius={40}
            outerRadius={80}
            dataKey="value"
          >
            {data.map((entry, index) => (
              <Cell
                key={`cell-${index}`}
                fill={COLORS[index % COLORS.length]}
              />
            ))}
          </Pie>
        </PieChart>
      </ResponsiveContainer>
      <Flex direction="column" gap="1">
        {data.map((entry, index) => (
          <Flex key={index}>
            <Flex flexGrow="1" align="center" gap="2">
              <Box
                height={"10px"}
                width={"10px"}
                style={{
                  backgroundColor: COLORS[index % COLORS.length],
                  borderRadius: "3px",
                }}
              />
              <Text size="1" weight="medium" color="gray">
                {entry.name}
                {` (${+Math.round(
                  (entry.value /
                    data.reduce((acc, curr) => acc + curr.value, 0)) *
                    100
                )}%)`}
              </Text>
            </Flex>
            <Text size="1" weight="bold">
              {entry.value}
            </Text>
          </Flex>
        ))}
      </Flex>
    </ChartCard>
  );
};
