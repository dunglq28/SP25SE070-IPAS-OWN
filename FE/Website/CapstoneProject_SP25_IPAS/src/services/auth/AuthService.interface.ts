export interface IAuthService {
  login(email: string, password: string): Promise<any>;
  refreshToken(): Promise<string | undefined>;
  //   logout(): Promise<void>;
}
