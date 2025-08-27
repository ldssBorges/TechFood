declare module "axios" {
  interface AxiosRequestConfig {
    silent?: boolean;
  }
}

export interface AxiosRequestConfig {
  silent?: boolean;
}
