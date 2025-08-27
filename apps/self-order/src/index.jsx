import ReactDOM from "react-dom/client";
import { registerSW } from "virtual:pwa-register";
import { I18nProvider } from "./i18n";

import App from "./App";

import "./index.css";

registerSW();

const container = document.getElementById("root");
const root = ReactDOM.createRoot(container);

root.render(
  <I18nProvider>
    <App />
  </I18nProvider>
);
