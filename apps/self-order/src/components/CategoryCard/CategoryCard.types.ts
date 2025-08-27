import { Category } from "../../models";

export type CategoryCardProps = {
  item: Category;
  selected?: boolean;
  onClick?: () => void;
};
