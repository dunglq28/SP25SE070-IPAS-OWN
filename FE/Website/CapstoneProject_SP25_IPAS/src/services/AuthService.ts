import { LoginResponse } from "@/payloads";
import { IAuthService } from "./AuthService.interface";
import { axiosNoAuth } from "@/api";

export class AuthService implements IAuthService {
  // Ctrl + . to import implementation
  async login(email: string, password: string): Promise<any> {
    const res = await axiosNoAuth.post("authentication/login", {
      email: email,
      password: password,
    });
    const apiResponse = res.data as LoginResponse;
    return apiResponse;
  }

  async refreshToken(): Promise<string | undefined> {
    throw new Error("Method not implemented.");
  }
}
