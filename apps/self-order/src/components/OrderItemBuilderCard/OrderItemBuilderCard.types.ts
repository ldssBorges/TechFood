import { OrderItem, Product } from "../../models";

export type OrderItemBuilderCardProps = {
  item: Product;
  onClose: () => void;
  onAdd: (item: Omit<OrderItem, "id">) => void;
};
