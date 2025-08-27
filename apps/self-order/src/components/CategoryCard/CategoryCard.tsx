import { Box, Flex, Text } from "@radix-ui/themes";
import { clsx } from "clsx";
import { CategoryCardProps } from "./CategoryCard.types";

import classNames from "./CategoryCard.module.css";

export const CategoryCard = ({
  item: { name, imageUrl },
  selected,
  onClick,
}: CategoryCardProps) => {
  return (
    <Flex
      className={clsx(classNames.root, selected && classNames.selected)}
      direction="column"
      align="center"
      justify="center"
      onClick={onClick}
    >
      <Box className={classNames.imageContainer}>
        <img src={imageUrl} alt={name} />
      </Box>
      <Text as="p" size="1" weight="medium" color="gray">
        {name}
      </Text>
    </Flex>
  );
};
