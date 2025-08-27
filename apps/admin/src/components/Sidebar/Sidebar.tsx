import { NavLink } from "react-router";
import { Flex, Text } from "@radix-ui/themes";
import clsx from "clsx";
import { SidebarItem } from "./Sidebar.types";

import classNames from "./Sidebar.module.css";

const Item = ({ title, path, icon }: SidebarItem) => {
  return (
    <NavLink
      to={path}
      end
      className={({ isActive }) =>
        clsx(classNames.item, isActive ? `${classNames.item}--selected` : "")
      }
    >
      <Flex align="center" gap="2" className={classNames.icon}>
        {icon && icon}
        <Text as="span" size="3" weight="medium">
          {title}
        </Text>
      </Flex>
    </NavLink>
  );
};

const Root = ({ children }: { children: React.ReactNode }) => {
  return <div className={classNames.root}>{children}</div>;
};

export const Sidebar = Object.assign(Root, {
  Item,
});
