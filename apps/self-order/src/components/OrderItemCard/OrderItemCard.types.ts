import { OrderItem, Product } from "../../models";

export type OrderItemCardProps = {
  item: OrderItem;
  product: Product;
  onRemove: (item: OrderItem) => void;
  onUpdate: (item: OrderItem) => void;
};
