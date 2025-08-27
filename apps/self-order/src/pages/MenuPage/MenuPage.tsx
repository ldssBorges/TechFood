import { useEffect, useState } from "react";
import {
  Box,
  Flex,
  Heading,
  Select,
  Strong,
  TextField,
  Text,
  Button,
  Spinner,
} from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import axios from "axios";
import { useNavigate } from "react-router";
import { t } from "../../i18n";
import { Category, Product } from "../../models";
import {
  CategoryCard,
  ProductCard,
  OrderItemBuilderCard,
  OrderItemCard,
  LanguageSwitch,
} from "../../components";
import { useOrder } from "../../contexts";

import classNames from "./MenuPage.module.css";

const sortByOptions = [
  { value: "name", label: () => t("menuPage.sort.name") },
  { value: "price", label: () => t("menuPage.sort.price") },
];

const CategoriesCard = ({
  items,
  selectedItem,
  onSelectedItem,
}: {
  items: Category[];
  selectedItem?: Category;
  onSelectedItem: (item: Category) => void;
}) => {
  return (
    <Flex
      className={classNames.categoriesCard}
      direction="row"
      gap="4"
      overflowX="auto"
    >
      {items.map((item: Category) => (
        <CategoryCard
          key={item.id}
          item={item}
          selected={item == selectedItem}
          onClick={() => {
            onSelectedItem(item);
          }}
        />
      ))}
    </Flex>
  );
};

const ItemsCard = ({
  items,
  onSelectedItem,
}: {
  items: Product[];
  onSelectedItem: (item: Product) => void;
}) => {
  return (
    <Flex
      className={classNames.itemsCard}
      direction="column"
      gap="4"
      overflow="hidden"
    >
      <Flex direction="row" justify="between">
        <Heading size="5" as="h1" weight="regular">
          <Strong>{t("menuPage.choose")}</Strong> {t("menuPage.order")}
        </Heading>
        <Flex align="center" gap="1">
          <Text size="1">{t("menuPage.sortBy")}</Text>
          <Select.Root defaultValue={sortByOptions[0].value} size="1">
            <Select.Trigger className={classNames.sort} variant="ghost" />
            <Select.Content>
              {sortByOptions.map((option) => (
                <Select.Item key={option.value} value={option.value}>
                  {option.label()}
                </Select.Item>
              ))}
            </Select.Content>
          </Select.Root>
        </Flex>
      </Flex>
      <Flex
        className={classNames.items}
        direction="row"
        gap="4"
        wrap="wrap"
        overflowY="auto"
      >
        {items.map((item, i) => (
          <ProductCard
            key={i}
            item={item}
            onClick={() => onSelectedItem(item)}
          />
        ))}
      </Flex>
    </Flex>
  );
};

export const MenuPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedProduct, setSelectedProduct] = useState<Product | undefined>();
  const [selectedCategory, setSelectedCategory] = useState<
    Category | undefined
  >();

  const productsByCategory = products.filter(
    (product) => product.categoryId === selectedCategory?.id
  );

  const navigate = useNavigate();

  const { items, total, addItem, updateItem, removeItem, createOrder } =
    useOrder();

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);

      const [categories, products] = await Promise.all([
        axios.get<Category[]>("/api/v1/categories"),
        axios.get<Product[]>("/api/v1/products"),
      ]);

      setProducts(products.data);
      setCategories(categories.data);
      setSelectedCategory(categories.data[0]);
      setIsLoading(false);
    };

    fetchData();
  }, []);

  const handleDone = async () => {
    await createOrder();
    navigate("/checkout");
  };

  return isLoading ? (
    <Spinner size="3" />
  ) : (
    <Flex className={classNames.root} direction="row">
      <Flex className={classNames.left} direction="column" gap="4" flexGrow="1">
        <Flex direction="row" justify="between">
          <Heading size="5" as="h1" weight="regular">
            <Strong>{t("menuPage.menu")}</Strong> {t("menuPage.category")}
          </Heading>
          <Box width="100%" maxWidth="500px">
            <TextField.Root
              className={classNames.search}
              placeholder={t("menuPage.search")}
              size="2"
            >
              <TextField.Slot>
                <MagnifyingGlassIcon height="25" width="25" />
              </TextField.Slot>
            </TextField.Root>
          </Box>
        </Flex>
        <CategoriesCard
          items={categories}
          selectedItem={selectedCategory}
          onSelectedItem={setSelectedCategory}
        />
        <ItemsCard
          items={productsByCategory}
          onSelectedItem={setSelectedProduct}
        />
        {selectedProduct && (
          <OrderItemBuilderCard
            item={selectedProduct}
            onClose={() => setSelectedProduct(undefined)}
            onAdd={(item) => {
              addItem(item);
              setSelectedProduct(undefined);
            }}
          />
        )}
      </Flex>
      <Flex className={classNames.right} direction="column">
        <Flex className={classNames.rightHeader} direction="column" gap="4">
          <Flex direction="column" align="end">
            <LanguageSwitch />
          </Flex>
          <Heading as="h2" size="5" style={{ maxWidth: "140px" }}>
            {t("menuPage.myOrder")}
          </Heading>
        </Flex>
        <Flex
          className={classNames.rightContent}
          direction="column"
          overflowY="auto"
          flexGrow="1"
        >
          {items.map((item, i) => (
            <OrderItemCard
              key={i}
              item={item}
              product={products.find((i) => i.id === item.productId) as Product}
              onRemove={() => removeItem(item)}
              onUpdate={(item) => updateItem(item)}
            />
          ))}
        </Flex>
        <Flex
          className={classNames.rightFooter}
          direction="column"
          gap="2"
          align="center"
        >
          <Text size="2">{t("labels.total")}</Text>
          <Text size="3" weight="bold">
            {t("labels.currency")}
            {total.toFixed(2)}
          </Text>
          <Button
            size="4"
            className={classNames.doneButton}
            disabled={!items.length}
            onClick={handleDone}
          >
            {t("labels.done")}
          </Button>
        </Flex>
      </Flex>
    </Flex>
  );
};
