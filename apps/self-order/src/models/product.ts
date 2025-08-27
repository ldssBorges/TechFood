import { Garnish } from "./garnish";

export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  categoryId: string;
  unit?: string;
  outOfStock: boolean;
  garnishes: Garnish[];
}
