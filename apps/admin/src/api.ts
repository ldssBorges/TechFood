import axios, { AxiosError } from "axios";
import type { AxiosRequestConfig } from "axios";
import { toast } from "react-toastify";
import { t } from "./i18n";

const api = axios.create({
  baseURL: "/api",
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (
    error: AxiosError & { config?: AxiosRequestConfig & { silent?: boolean } }
  ) => {
    const status = error.response?.status;
    const silent = error.config?.silent;
    const message = (error.response?.data as any)?.message;

    if (!silent) {
      if (status === 401) {
        toast.error(t("errors.expiredSession"));
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        window.location.reload();
      } else if (status === 403) {
        toast.error(t("errors.unauthorizedAccess"));
      } else if (status === 500) {
        toast.error(t("errors.internalServerError"));
      } else if (status === 404) {
        toast.error(t("errors.notFound"));
      } else {
        toast.error(message || t("errors.unexpectedError"));
      }
    }

    return Promise.reject(error);
  }
);

export default api;
