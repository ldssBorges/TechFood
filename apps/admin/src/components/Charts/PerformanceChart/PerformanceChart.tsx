import { Cell, Pie, PieChart, ResponsiveContainer } from "recharts";
import { ChartCard } from "../ChartCard";

const COLORS = ["#f8b602", "#ededed"];

const data = [
  { name: "Group A", value: 400 },
  { name: "Group B", value: 300 },
];

export const PerformanceChart = () => {
  return (
    <ChartCard title="Performance" height="200px">
      <ResponsiveContainer width="100%" height="100%">
        <PieChart>
          <Pie
            dataKey="value"
            startAngle={180}
            endAngle={0}
            data={data}
            cx="50%"
            cy="80%"
            innerRadius={40}
            outerRadius={80}
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
    </ChartCard>
  );
};
