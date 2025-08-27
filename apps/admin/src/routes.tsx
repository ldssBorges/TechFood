import { createBrowserRouter } from "react-router";
import { MonitorDot, SquareKanbanIcon, UtensilsIcon } from "lucide-react";
import { AdminLayout } from "./components";
import { Dashboard, Forbidden, MenuManagement, SignIn } from "./pages";
import { Monitor } from "./pages/Monitor";

const router = createBrowserRouter(
  [
    {
      path: "/",
      element: <AdminLayout />,
      children: [
        {
          index: true,
          element: <Dashboard />,
          handle: {
            title: "Dashboard",
            menu: true,
            roles: ["admin"],
            icon: <SquareKanbanIcon />,
          },
        },
        {
          path: "menu",
          handle: {
            title: "Menu",
            menu: true,
            roles: ["admin"],
            icon: <UtensilsIcon />,
          },
          element: <MenuManagement />,
        },
        {
          path: "Monitor",
          element: <Monitor />,
          handle: { title: "Monitor", menu: true, icon: <MonitorDot /> },
        },
        // {
        //   path: "reviews",
        //   element: <MenuManagement />,
        //   handle: { title: "Reviews", menu: true, icon: <StarIcon /> },
        // },
      ],
    },
    {
      path: "/signin",
      element: <SignIn />,
    },
    {
      path: "/forbidden",
      element: <Forbidden />,
    },
  ],
  {
    basename: "/admin",
  }
);

export default router;
