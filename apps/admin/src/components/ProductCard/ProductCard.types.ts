import { Product } from "../../models";

export type ProductCardProps = {
  product: Product;
  onDeleteClick: (id: string) => void;
  onEditClick: (product: Product) => void;
};
