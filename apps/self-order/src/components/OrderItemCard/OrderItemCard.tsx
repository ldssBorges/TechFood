import { Flex, Text, Heading, IconButton } from "@radix-ui/themes";
import { MinusIcon, PlusIcon, Trash2Icon } from "lucide-react";
import { t } from "../../i18n";
import { OrderItemCardProps } from "./OrderItemCard.types";

import classNames from "./OrderItemCard.module.css";

export const OrderItemCard = ({
  item,
  product: { name, imageUrl, price },
  onRemove,
  onUpdate,
}: OrderItemCardProps) => {
  const isDeleteVisible = item.quantity <= 1;

  const handleQtyChange = (newQty: number) => {
    if (newQty < 1) {
      onRemove(item);
      return;
    }

    item.quantity = newQty;

    onUpdate(item);
  };

  return (
    <Flex className={classNames.root} direction="column" align="center">
      <img src={imageUrl} alt={name} />
      <Heading size="1" weight="bold" align="center">
        {name}
      </Heading>
      <Text size="1" color="gray">
        {t("labels.currency")}
        {price}
      </Text>
      <Flex direction="column" align="center">
        <Flex direction="row" gap="3" align="center">
          <IconButton
            variant="soft"
            color={isDeleteVisible ? "red" : "gray"}
            size="1"
            onClick={() => handleQtyChange(item.quantity - 1)}
          >
            {isDeleteVisible ? <Trash2Icon /> : <MinusIcon />}
          </IconButton>
          <Text size="1" color="gray">
            {item.quantity}
          </Text>
          <IconButton
            size="1"
            onClick={() => handleQtyChange(item.quantity + 1)}
          >
            <PlusIcon />
          </IconButton>
        </Flex>
      </Flex>
    </Flex>
  );
};
