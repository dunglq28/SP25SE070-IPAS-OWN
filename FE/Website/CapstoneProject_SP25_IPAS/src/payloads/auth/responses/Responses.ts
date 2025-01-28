export interface UserAuthResponse {
  accessToken: string;
  refreshToken: string;
  avatar: string;
  fullName: string;
}

export interface LoginResponse extends UserAuthResponse {}

export interface RegisterResponse extends UserAuthResponse {}

export interface OtpResponse {
  otpHash: string;
}
