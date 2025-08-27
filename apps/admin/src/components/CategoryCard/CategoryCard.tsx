import { Flex, Text } from "@radix-ui/themes";
import { CategoryCardProps } from "./CategoryCard.types";
import clsx from "clsx";

import classNames from "./CategoryCard.module.css";

export const CategoryCard = ({
  category,
  onSelect,
  selected = false,
}: CategoryCardProps) => {
  const { name, imageUrl } = category;
  return (
    <Flex
      className={clsx(classNames.root, selected && classNames.selected)}
      direction="column"
      gap="2"
      align="center"
      justify="center"
      onClick={() => onSelect(category)}
    >
      <img src={imageUrl} alt={name} />
      <Text
        as="p"
        size="2"
        weight="medium"
        color="gray"
        className={classNames.name}
      >
        {name}
      </Text>
    </Flex>
  );
};
