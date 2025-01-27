import { useState } from "react";

const useAuth = () => {
  const getAuthData = () => {
    // Lấy dữ liệu từ localStorage, trả về đối tượng
    const authData = {
      accessToken: localStorage.getItem("accessToken"),
      refreshToken: localStorage.getItem("refreshToken"),
      fullname: localStorage.getItem("fullname"),
      avatar: localStorage.getItem("avatar"),
    };
    return authData;
  };

  const saveAuthData = (data: {
    accessToken: string;
    refreshToken: string;
    fullname: string;
    avatar: string;
  }) => {
    // Lưu vào localStorage
    localStorage.setItem("accessToken", data.accessToken);
    localStorage.setItem("refreshToken", data.refreshToken);
    localStorage.setItem("fullname", data.fullname);
    localStorage.setItem("avatar", data.avatar);
  };

  const clearAuthData = () => {
    // Xóa dữ liệu khỏi localStorage
    localStorage.clear();
  };

  return {
    getAuthData,
    saveAuthData,
    clearAuthData,
  };
};

export default useAuth;
