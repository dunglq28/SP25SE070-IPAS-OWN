import { ApiResponse, LoginResponse, OtpResponse } from "@/payloads";
import { axiosNoAuth } from "@/api";

export const loginGoogle = async (googleToken: string): Promise<ApiResponse<LoginResponse>> => {
  const res = await axiosNoAuth.post("login-with-google", {
    googleToken: googleToken,
  });
  const apiResponse = res.data as ApiResponse<LoginResponse>;
  return apiResponse;
};

export const login = async (
  email: string,
  password: string,
): Promise<ApiResponse<LoginResponse>> => {
  const res = await axiosNoAuth.post("login", {
    email: email,
    password: password,
  });
  const apiResponse = res.data as ApiResponse<LoginResponse>;
  return apiResponse;
};

export const sendOTP = async (email: string): Promise<ApiResponse<OtpResponse>> => {
  const res = await axiosNoAuth.post("register/send-otp", {
    email: email,
  });
  const apiResponse = res.data as ApiResponse<OtpResponse>;
  return apiResponse;
};
