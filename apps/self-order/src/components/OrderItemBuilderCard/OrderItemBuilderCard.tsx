import { useState } from "react";
import { Button, Flex, Grid, IconButton, Text } from "@radix-ui/themes";
import { MinusIcon, PlusIcon } from "lucide-react";
import { clsx } from "clsx";
import { t } from "../../i18n";
import { Garnish } from "../../models";
import { OrderItemBuilderCardProps } from "./OrderItemBuilderCard.types";

import classNames from "./OrderItemBuilderCard.module.css";
import { BottomSheet } from "../BottomSheet";

const GarnisheItem = ({ name, description, img }: Garnish) => {
  const [count, setCount] = useState(1);

  const src = new URL(`../../assets/products/${img}.png`, import.meta.url).href;

  const handleCountChange = (newCount: number) => {
    if (newCount < 0) return;
    setCount(newCount);
  };

  return (
    <Grid
      className={classNames.garnisheItem}
      columns="auto 1fr auto"
      gap="2"
      align="center"
    >
      <img src={src} />
      <Flex direction="column" gap="1" justify="center">
        <Text size="2">{name}</Text>
        <Text size="1" color="gray">
          {description}
        </Text>
      </Flex>
      <Flex direction="row" gap="4" align="center">
        <IconButton
          variant="soft"
          color="gray"
          size="1"
          onClick={() => handleCountChange(count - 1)}
          disabled={count <= 1}
        >
          <MinusIcon />
        </IconButton>
        <Text size="1" color="gray">
          {count}
        </Text>
        <IconButton size="1" onClick={() => handleCountChange(count + 1)}>
          <PlusIcon />
        </IconButton>
      </Flex>
    </Grid>
  );
};

const GarnisheList = ({ items }: { items: Garnish[] }) => {
  return (
    <Flex className={classNames.garnisheList} direction="column" gap="4">
      <Flex className={classNames.garnisheListItems} direction="column" gap="2">
        {items.map((item: any) => (
          <GarnisheItem key={item.id} {...item} />
        ))}
      </Flex>
      <Flex
        className={classNames.garnisheListButtons}
        direction="row"
        justify="center"
      >
        <Button size="2">{t("labels.apply")}</Button>
      </Flex>
    </Flex>
  );
};

export const OrderItemBuilderCard = ({
  item: { id, name, price, imageUrl, unit, garnishes },
  onClose,
  onAdd,
}: OrderItemBuilderCardProps) => {
  const [count, setCount] = useState(1);
  const [isChosingGarnishe, setIsChoosingGarnishe] = useState(false);

  const handleCountChange = (newCount: number) => {
    if (newCount < 0) return;
    setCount(newCount);
  };

  return (
    <BottomSheet className={classNames.root} onClose={onClose}>
      <Flex
        direction="column"
        align="center"
        className={clsx(
          classNames.card,
          isChosingGarnishe && classNames.chosingGarnishe
        )}
      >
        <Flex
          className={classNames.itemInfo}
          direction="column"
          gap="4"
          align="center"
        >
          <img src={imageUrl} alt={name} className={classNames.img} />
          <Flex direction="column" align="center">
            <Text size="2" weight="bold">
              {name}
            </Text>
            <Text size="1" color="gray">
              {unit}
            </Text>
            <Text size="2" weight="bold" className={classNames.price}>
              {t("labels.currency")}
              {price}
            </Text>
          </Flex>
        </Flex>
        {isChosingGarnishe ? (
          <GarnisheList items={garnishes} />
        ) : (
          <Flex direction="column" gap="5" align="center">
            <Flex direction="row" gap="4" align="center">
              <IconButton
                variant="soft"
                color="gray"
                size="1"
                onClick={() => handleCountChange(count - 1)}
                disabled={count <= 1}
              >
                <MinusIcon />
              </IconButton>
              <Text size="1" color="gray">
                {count}
              </Text>
              <IconButton size="1" onClick={() => handleCountChange(count + 1)}>
                <PlusIcon />
              </IconButton>
            </Flex>
            <Flex direction="row" gap="4" justify="center">
              {garnishes && garnishes.length > 0 && (
                <Button
                  variant="soft"
                  color="gray"
                  size="2"
                  onClick={() => setIsChoosingGarnishe(!isChosingGarnishe)}
                >
                  {t("labels.customize")}
                </Button>
              )}
              <Button
                size="2"
                onClick={() =>
                  onAdd({
                    productId: id,
                    quantity: count,
                    unitPrice: price,
                  })
                }
              >
                {t("labels.done")}
              </Button>
            </Flex>
          </Flex>
        )}
      </Flex>
    </BottomSheet>
  );
};
