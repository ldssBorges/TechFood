import { createBrowserRouter } from "react-router";
import {
  StartPage,
  MenuPage,
  CheckoutPage,
  RegisterPage,
  ConfirmationPage,
  HomePage,
} from "./pages";

const router = createBrowserRouter(
  [
    {
      index: true,
      path: "/",
      element: <HomePage />,
    },
    {
      path: "/start",
      element: <StartPage />,
    },
    {
      path: "menu",
      element: <MenuPage />,
    },
    {
      path: "checkout",
      element: <CheckoutPage />,
    },
    {
      path: "register/:doc",
      element: <RegisterPage />,
    },
    {
      path: "confirmation",
      element: <ConfirmationPage />,
    },
  ],
  {
    basename: "/self-order",
  }
);

export default router;
