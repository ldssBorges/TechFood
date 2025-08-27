import { Product } from "../../models";

export type ProductCardProps = {
  item: Product;
  onClick?: () => void;
};
