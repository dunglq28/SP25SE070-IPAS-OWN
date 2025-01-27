import { toast } from "react-toastify";

export const handleApiError = (error: any) => {
  if (error.message === "Network Error" && !error.response) {
    toast.error("Network error, please check your connection!");
  }
  //   else if (error.response) {
  //     switch (error.response.status) {
  //       case 400:
  //         toast.error("Login failed");
  //         break;
  //       case 401:
  //       case 403:
  //         toast.error(error.response.data.message);
  //         break;
  //       default:
  //         toast.error("An error occurred");
  //     }
  //   }
  return Promise.reject(error);
};
