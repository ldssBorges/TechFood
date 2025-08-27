import { Theme } from "@radix-ui/themes";
import { RouterProvider } from "react-router";
import router from "./routes";

import "@radix-ui/themes/styles.css";
import "./App.css";

function App() {
  return (
    <Theme accentColor="amber" radius="large" grayColor="sage">
      <RouterProvider router={router} />
    </Theme>
  );
}

export default App;
