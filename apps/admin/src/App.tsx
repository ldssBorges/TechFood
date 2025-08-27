import { Theme } from "@radix-ui/themes";
import { RouterProvider } from "react-router";
import router from "./routes";
import { ToastContainer } from "react-toastify";

import "@radix-ui/themes/styles.css";
import "./App.css";

function App() {
  return (
    <Theme accentColor="amber" radius="large" grayColor="sage">
      <RouterProvider router={router} />
      <ToastContainer
        position="bottom-right"
        autoClose={3000}
        hideProgressBar={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </Theme>
  );
}

export default App;
