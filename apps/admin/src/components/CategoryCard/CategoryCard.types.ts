import { Category } from "../../models";

export type CategoryCardProps = {
  category: Category;
  selected?: boolean;
  onSelect: (category: Category) => void;
};
