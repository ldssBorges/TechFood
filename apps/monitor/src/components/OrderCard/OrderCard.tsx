import { Flex, Heading } from "@radix-ui/themes";
import classNames from "./OrderCard.module.css";
import { TOrderCardProps } from "./OrderCard.types";

export const OrderCard = ({ orders, title, type }: TOrderCardProps) => {
  return (
    <Flex direction="column" className={classNames.container}>
      <Flex className={classNames[type]}>
        <Heading as="h2" size="8">
          {title}
        </Heading>
      </Flex>
      <Flex wrap={"wrap"} gap="6" className={classNames.order}>
        {orders.map((order) => (
          <Flex key={order.number}>
            <Heading as="h3" size="8">
              {order.number}
            </Heading>
          </Flex>
        ))}
      </Flex>
    </Flex>
  );
};
