import {
  ApiResponse,
  LoginResponse,
  OtpResponse,
  RegisterRequest,
  RegisterResponse,
} from "@/payloads";
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

export const register = async (
  registerRequest: RegisterRequest,
): Promise<ApiResponse<RegisterResponse>> => {
  const { email, password, fullName, phone, gender, dob } = registerRequest;
  const res = await axiosNoAuth.post("register", {
    email,
    password,
    fullName,
    gender,
    phone,
    dob,
  });
  const apiResponse = res.data as ApiResponse<RegisterResponse>;
  return apiResponse;
};

export const logout = async (refreshToken: string): Promise<ApiResponse<Object>> => {
  const res = await axiosNoAuth.post("logout", {
    refreshToken,
  });
  const apiResponse = res.data as ApiResponse<Object>;
  return apiResponse;
};
