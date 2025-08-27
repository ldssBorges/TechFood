import { useEffect, useMemo, useState } from "react";
import {
  Flex,
  Heading,
  TextField,
  Button,
  Dialog,
  Box,
  Text,
  TextArea,
  Select,
} from "@radix-ui/themes";
import * as Label from "@radix-ui/react-label";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import api from "../../api";
import { t } from "../../i18n";
import { normalizeText } from "../../utilities";
import { Product, Category, Menu } from "../../models/";
import { ProductFormData, productSchema } from "../../schemas";
import {
  CategoryCard,
  ProductCard,
  AlertDialog,
  CurrencyInput,
  FileInputWithPreview,
} from "../../components";

import classNames from "./MenuManagement.module.css";

const Section = ({ title, direction, children }: any) => {
  return (
    <Flex className={classNames.section} direction="column" gap="4">
      <Heading size="4" as="h2">
        {title}
      </Heading>
      <Flex gap="4" wrap="wrap" direction={direction}>
        {children}
      </Flex>
    </Flex>
  );
};

const DeleteProductDialog = ({
  product,
  onConfirm,
  onCancel,
}: {
  product: Product;
  onConfirm: (product: Product) => void;
  onCancel: () => void;
}) => {
  return (
    <AlertDialog
      title={t("menuManagementPage.deleteDialog.title")}
      description={t("menuManagementPage.deleteDialog.message", {
        name: product.name,
      })}
      open={true}
      onConfirm={() => onConfirm(product)}
      onCancel={onCancel}
    />
  );
};

const EditProductDialog = ({
  product,
  categories,
  onSave,
  onCancel,
}: {
  product: Product | null;
  categories: Category[];
  onSave: (form: FormData) => void;
  onCancel: () => void;
}) => {
  const {
    register,
    handleSubmit,
    getValues,
    control,
    reset,
    formState: { errors },
  } = useForm<ProductFormData>({
    resolver: zodResolver(productSchema),
    defaultValues: {
      name: "",
      description: "",
      categoryId: "",
      file: undefined,
      price: undefined,
      imageUrl: "",
    },
  });

  useEffect(() => {
    if (product) {
      reset({
        name: product.name,
        description: product.description,
        categoryId: product.categoryId,
        file: undefined,
        price: product.price,
        imageUrl: product.imageUrl,
      });
    } else {
      reset({
        name: "",
        description: "",
        categoryId: "",
        file: undefined,
        price: undefined,
        imageUrl: "",
      });
    }
  }, [product, reset]);

  const onSubmit = async (data: ProductFormData) => {
    const formData = new FormData();
    formData.append("Name", data.name);
    formData.append("Description", data.description);
    formData.append("CategoryId", data.categoryId);
    formData.append("Price", String(data.price));
    formData.append("File", data.file ? data.file[0] : "");

    onSave(formData);
  };

  return (
    <Dialog.Root open={true}>
      <Dialog.Content maxWidth="450px">
        <form onSubmit={handleSubmit(onSubmit)}>
          <Dialog.Title>
            {product
              ? t("menuManagementPage.editDialog.editTitle")
              : t("menuManagementPage.editDialog.addTitle")}
          </Dialog.Title>
          <Flex direction="column" gap="3">
            <Box>
              <Label.Root htmlFor="name">{t("labels.name")}</Label.Root>
              <TextField.Root id="name" {...register("name")} />
              {errors.name && <Text color="red">{errors.name.message}</Text>}
            </Box>

            <Box>
              <Label.Root htmlFor="description">
                {t("labels.description")}
              </Label.Root>
              <Controller
                control={control}
                name="description"
                render={({ field }) => <TextArea id="description" {...field} />}
              />
              {errors.description && (
                <Text color="red">{errors.description.message}</Text>
              )}
            </Box>

            <Flex direction="column">
              <Controller
                control={control}
                name="categoryId"
                render={({ field }) => (
                  <>
                    <Label.Root htmlFor="categoryId">
                      {t("labels.category")}
                    </Label.Root>
                    <Select.Root
                      onValueChange={field.onChange}
                      value={field.value}
                    >
                      <Select.Trigger
                        className={classNames.selectButton}
                        placeholder={t(
                          "menuManagementPage.editDialog.selectCategory"
                        )}
                      />
                      <Select.Content position="popper">
                        <Select.Group>
                          {categories.map((category) => (
                            <Select.Item key={category.id} value={category.id}>
                              {category.name}
                            </Select.Item>
                          ))}
                        </Select.Group>
                      </Select.Content>
                    </Select.Root>
                  </>
                )}
              />
              {errors.categoryId && (
                <Text color="red">{errors.categoryId.message}</Text>
              )}
            </Flex>

            <Box>
              <Label.Root htmlFor="price">{t("labels.price")}</Label.Root>
              <Controller
                control={control}
                name="price"
                defaultValue={undefined}
                render={({ field }) => (
                  <CurrencyInput
                    id="price"
                    name="price"
                    value={field.value}
                    onChange={field.onChange}
                    error={errors.price?.message}
                  />
                )}
              />
            </Box>

            <Box>
              <Controller
                control={control}
                name="file"
                render={({ field }) => (
                  <FileInputWithPreview
                    name="file"
                    value={field.value}
                    onChange={field.onChange}
                    error={errors.file}
                    imageUrl={getValues("imageUrl")}
                  />
                )}
              />
            </Box>

            <Flex gap="3" mt="4" justify="end">
              <Dialog.Close>
                <Button variant="soft" color="gray" onClick={onCancel}>
                  {t("labels.cancel")}
                </Button>
              </Dialog.Close>
              <Button type="submit">{t("labels.save")}</Button>
            </Flex>
          </Flex>
        </form>
      </Dialog.Content>
    </Dialog.Root>
  );
};

export const MenuManagement = () => {
  const [menu, setMenu] = useState<Menu | null>(null);
  const [search, setSearch] = useState("");
  const [selectedCategory, setSelectedCategory] = useState<Category | null>(
    null
  );
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [action, setAction] = useState<"edit" | "delete" | null>(null);

  useEffect(() => {
    const fetch = async () => {
      const menu = await api.get<Menu>("/v1/menu");
      setMenu(menu.data);
    };

    fetch();
  }, []);

  const handleSaveProduct = async (formData: FormData) => {
    const isNewProduct = !selectedProduct?.id;
    const { data } = await api.request<Product>({
      method: isNewProduct ? "post" : "put",
      url: `/v1/Products/${selectedProduct?.id || ""}`,
      data: formData,
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });

    const msgKey = isNewProduct ? "add" : "edit";

    toast.success(t(`menuManagementPage.editDialog.${msgKey}SuccessMessage`));

    setMenu((prev) => {
      return {
        ...prev,
        categories: prev!.categories.map((category) => {
          const newProducts = category.products.filter(
            (p) => p.id !== selectedProduct?.id
          );

          if (category.id === data.categoryId) {
            const index = category.products.findIndex(
              (p) => p.id === selectedProduct?.id
            );
            if (index !== -1) {
              newProducts.splice(index, 0, data);
            } else {
              newProducts.push(data);
            }
            return { ...category, products: newProducts };
          }

          return { ...category, products: [...newProducts] };
        }),
      };
    });
    clearAction();
  };

  const handleDeleteProduct = async (product: Product) => {
    await api.delete(`/v1/products/${product.id}`);

    setMenu((prev) => {
      if (!prev) return null;
      const newCategories = prev.categories.map((category) => {
        const newProducts = category.products.filter(
          (p) => p.id !== product.id
        );
        return { ...category, products: newProducts };
      });
      return { ...prev, categories: newCategories };
    });
    clearAction();
  };

  const clearAction = () => {
    setAction(null);
    setSelectedProduct(null);
  };

  const products = useMemo(() => {
    if (!menu) return [];

    const allProducts = menu.categories.flatMap(
      (category) => category.products
    );

    return allProducts.filter((product) => {
      const matchesSearch = search
        ? normalizeText(product.name).includes(normalizeText(search))
        : true;

      const matchesCategory =
        !search && selectedCategory
          ? product.categoryId === selectedCategory.id
          : true;

      return matchesSearch && matchesCategory;
    });
  }, [menu, search, selectedCategory]);

  return menu ? (
    <Flex direction="column">
      <Flex gap="8" align="center">
        <TextField.Root
          className={classNames.search}
          placeholder="Search"
          size="3"
          onChange={(e) => setSearch(e.target.value)}
        >
          <TextField.Slot>
            <MagnifyingGlassIcon height="25" width="25" />
          </TextField.Slot>
        </TextField.Root>
        <Button
          size={"3"}
          onClick={() => {
            setAction("edit");
            setSelectedProduct(null);
          }}
        >
          {t("menuManagementPage.addProduct")}
        </Button>
      </Flex>
      <Flex direction="row" justify="between"></Flex>
      {!search && (
        <Section title={t("menuManagementPage.categories")}>
          {menu.categories.map((category) => (
            <CategoryCard
              key={category.id}
              category={category}
              selected={selectedCategory === category}
              onSelect={(category) => {
                if (selectedCategory === category) {
                  setSelectedCategory(null);
                  return;
                }
                setSelectedCategory(category);
              }}
            />
          ))}
        </Section>
      )}
      <Section title={t("menuManagementPage.products")}>
        {products.map((product) => (
          <ProductCard
            key={product.id}
            product={product}
            onDeleteClick={() => {
              setAction("delete");
              setSelectedProduct(product);
            }}
            onEditClick={() => {
              setAction("edit");
              setSelectedProduct(product);
            }}
          />
        ))}
      </Section>
      {action === "edit" && (
        <EditProductDialog
          categories={menu.categories}
          product={selectedProduct}
          onSave={handleSaveProduct}
          onCancel={clearAction}
        />
      )}
      {selectedProduct && action === "delete" && (
        <DeleteProductDialog
          product={selectedProduct}
          onConfirm={handleDeleteProduct}
          onCancel={clearAction}
        />
      )}
    </Flex>
  ) : null;
};
