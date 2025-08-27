import { Theme } from "@radix-ui/themes";
import { RouterProvider } from "react-router";
import { CustomerProvider, OrderProvider } from "./contexts";
import router from "./routes";

import "@radix-ui/themes/styles.css";
import "./App.css";

function App() {
  return (
    <Theme accentColor="amber" radius="large" grayColor="sage">
      <CustomerProvider>
        <OrderProvider>
          <RouterProvider router={router} />
        </OrderProvider>
      </CustomerProvider>
    </Theme>
  );
}

export default App;
