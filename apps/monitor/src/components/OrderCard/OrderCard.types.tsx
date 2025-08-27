import { Order } from "../../models/order";

export type TOrderCardProps = {
  orders: Order[];
  title: string;
  type: string;
};
