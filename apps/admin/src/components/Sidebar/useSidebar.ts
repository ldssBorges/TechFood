import { useMemo } from "react";
import { RouteObject } from "react-router";
import { SidebarItem } from "./Sidebar.types";
import router from "../../routes";

export const useSidebar = (route: string) => {
  const items = useMemo(
    () => generateFromRoutes(router.routes, route),
    [route]
  );

  return {
    items,
  };
};

export const generateFromRoutes = (
  routes: RouteObject[],
  pathBase: string,
  path = ""
): SidebarItem[] => {
  const menu: SidebarItem[] = [];

  routes.forEach((route) => {
    const fullPath = path + (route.path ?? "");

    if (!fullPath.startsWith(pathBase)) {
      return;
    }

    if (route.handle?.menu) {
      menu.push({
        path: fullPath,
        title: route.handle.title,
        icon: route.handle.icon,
      });
    }

    if (route.children) {
      const childMenu = generateFromRoutes(
        route.children,
        pathBase,
        fullPath.endsWith("/") ? fullPath : fullPath + "/"
      );
      menu.push(...childMenu);
    }
  });

  return menu;
};
