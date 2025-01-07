import axios, { AxiosInstance, AxiosResponse, InternalAxiosRequestConfig } from "axios";
import { toast } from "react-toastify";

const API_HOST = import.meta.env.VITE_API_HOST;
const API_PORT = import.meta.env.VITE_API_PORT;
const API_DEVELOPMENT = import.meta.env.VITE_API_DEVELOPMENT;
const API_DEPLOY = import.meta.env.VITE_API_DEPLOY;

const BASE_URL = API_DEVELOPMENT === "true" ? `${API_HOST}:${API_PORT}/api` : `${API_DEPLOY}/api`;

const createAxiosInstance = (contentType: string): AxiosInstance => {
  const instance = axios.create({
    baseURL: BASE_URL,
    headers: {
      "Content-Type": contentType,
    },
  });

  instance.interceptors.request.use(
    (config: InternalAxiosRequestConfig) => {
      config.headers = config.headers || {};

      const accessToken = localStorage.getItem("AccessToken");
      if (accessToken) {
        config.headers.Authorization = `Bearer ${accessToken}`;
      }

      return config;
    },
    (error) => Promise.reject(error),
  );

  instance.interceptors.response.use(
    (response: AxiosResponse) => response,
    async (error) => {
      if (error.message === "Network Error" && !error.response) {
        toast.error("Lỗi mạng, vui lòng kiểm tra kết nối!");
      }

      const originalRequest = error.config;

      if (error.response) {
        const { status, data } = error.response;

        if (status === 403) {
          toast.error("Bạn không có quyền truy cập vào tài nguyên này");
        }

        if (status === 401) {
          const authMessage = data?.Message;

          if (authMessage?.includes("Token đã hết hạn!")) {
            // Uncomment and implement refreshToken logic if needed
            // const newAccessToken = await AuthenticationService.refreshToken();
            // if (newAccessToken) {
            //   originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
            //   return instance(originalRequest);
            // } else {
            //   toast.error("Token đã hết hạn. Vui lòng đăng nhập lại.");
            //   localStorage.clear();
            //   window.location.href = "/login";
            // }
          } else {
            toast.error("Bạn không được phép truy cập trang này");
          }
        }
      }

      return Promise.reject(error);
    },
  );

  return instance;
};

const axiosMultipartForm = createAxiosInstance("multipart/form-data");
const axiosJsonRequest = createAxiosInstance("application/json");

export default { axiosMultipartForm, axiosJsonRequest };
