import moment from "moment";
import { UserRole } from "@/constants/Enum";
import { camelCase, kebabCase } from "change-case";
import { jwtDecode } from "jwt-decode";
import { DecodedToken } from "@/types";
import { LOCAL_STORAGE_KEYS } from "@/constants";

export const convertQueryParamsToKebabCase = (params: Record<string, any>): Record<string, any> => {
  const newParams: Record<string, any> = {};
  for (const key in params) {
    if (params.hasOwnProperty(key)) {
      newParams[kebabCase(key)] = params[key];
    }
  }
  return newParams;
};

export const convertKeysToKebabCase = (obj: any): any => {
  if (typeof obj !== "object" || obj === null) return obj;

  if (Array.isArray(obj)) {
    return obj.map((item) => convertKeysToKebabCase(item));
  }

  return Object.keys(obj).reduce((acc, key) => {
    const kebabKey = kebabCase(key);
    acc[kebabKey] = convertKeysToKebabCase(obj[key]);
    return acc;
  }, {} as any);
};

export const convertKeysToCamelCase = (obj: any): any => {
  if (typeof obj !== "object" || obj === null) return obj;

  if (Array.isArray(obj)) {
    return obj.map((item) => convertKeysToCamelCase(item));
  }

  return Object.keys(obj).reduce((acc, key) => {
    const camelKey = camelCase(key);
    acc[camelKey] = convertKeysToCamelCase(obj[key]);
    return acc;
  }, {} as any);
};

export const hashOtp = async (input: string): Promise<string> => {
  // Chuyển OTP thành mảng byte
  const encoder = new TextEncoder();
  const data = encoder.encode(input);

  // Tạo hash SHA-256
  const hashBuffer = await crypto.subtle.digest("SHA-256", data);

  // Chuyển buffer nhị phân thành Base64
  const hashArray = Array.from(new Uint8Array(hashBuffer));
  const base64String = btoa(String.fromCharCode(...hashArray));

  return base64String;
};

export const buildParams = (
  currentPage?: number,
  rowsPerPage?: number,
  sortField?: string,
  sortDirection?: string,
  searchValue?: string,
  brandId?: string | null,
  additionalParams?: Record<string, any>,
): Record<string, any> => {
  const params: Record<string, any> = {
    pageNumber: currentPage,
    pageSize: rowsPerPage,
    sortField,
    sortDirection,
    searchKey: searchValue,
    brandid: brandId,
    ...additionalParams,
  };

  Object.entries(params).forEach(([key, value]) => {
    if (Array.isArray(value)) {
      params[key] = value.filter((item) => item !== undefined && item !== null).join(",");
    }
  });

  return Object.fromEntries(
    Object.entries(params).filter(
      ([_, value]) =>
        value !== undefined &&
        value !== "" &&
        (!Array.isArray(value) || value.length > 0) &&
        (typeof value !== "object" || Object.keys(value).length > 0),
    ),
  );
};

export const isValidBreadcrumb = (path: string) => {
  const hasNumberAndSpecialChar = /\d/.test(path) && /[^a-zA-Z0-9]/.test(path);
  const hasNumberAndLetter = /[a-zA-Z]/.test(path) && /\d/.test(path);
  const isNumberOnly = /^\d+$/.test(path);

  return isNumberOnly || hasNumberAndSpecialChar || hasNumberAndLetter;
};

export const generateRandomKey = (): string => {
  return `${Math.random().toString(36).substr(2, 9)}-${Date.now()}`;
};

export const getOptions = (total: number): number[] => {
  if (total > 50) return [5, 10, 20, 50, 100];
  if (total > 20) return [5, 10, 20, 50];
  if (total > 10) return [5, 10, 20];
  if (total > 5) return [5, 10];
  return [5];
};

export const getBrandOptions = (total: number): number[] => {
  if (total > 50) return [6, 10, 20, 50, 100];
  if (total > 20) return [6, 10, 20, 50];
  if (total > 10) return [6, 10, 20];
  if (total > 6) return [6, 10];
  return [6];
};

export const formatCurrencyMenu = (amount: string): string => {
  const number = parseFloat(amount.replace(/,/g, ""));
  if (isNaN(number)) {
    return amount;
  }
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  })
    .format(number)
    .replace("₫", "")
    .replace(".000", "");
};

export const formatCurrencyVND = (amount: string): string => {
  const number = parseFloat(amount.replace(/,/g, ""));
  if (isNaN(number)) {
    return amount;
  }

  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  })
    .format(number)
    .replace("₫", "VND")
    .trim();
};

export const formatCurrency = (amount: string): string => {
  const number = parseFloat(amount.replace(/,/g, ""));
  if (isNaN(number)) {
    return amount;
  }

  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  })
    .format(number)
    .trim();
};

export const DATE_FORMAT = "DD/MM/YYYY";

export const getCurrentDate = (): string => {
  return moment().format("dddd, DD/MM/YYYY");
};

export const formatToISO8601 = (dateInput: string | Date): string => {
  if (!dateInput) {
    throw new Error("Invalid date input");
  }

  const formattedDate = moment(dateInput).toISOString();

  if (!formattedDate) {
    throw new Error("Failed to convert date to ISO 8601");
  }

  return formattedDate;
};

export const formatDate = (date: Date): string => {
  return moment(date).format("DD/MM/YYYY");
};

export const formatDateAndTime = (date: Date): string => {
  return moment(date).format("DD/MM/YYYY HH:mm:ss");
};

export const getRoleId = (): string => {
  const accessToken = localStorage.getItem(LOCAL_STORAGE_KEYS.ACCESS_TOKEN);
  if (!accessToken) return "";
  return jwtDecode<DecodedToken>(accessToken).roleId;
};

export const getRoleName = (roleId: number): string => {
  if (roleId === UserRole.Admin) {
    return "Quản trị viên";
  } else if (roleId === UserRole.Employee) {
    return "Quản lý thương hiệu";
  } else if (roleId === UserRole.Manager) {
    return "Quản lý chi nhánh";
  }
  return UserRole[roleId] ? `Vai trò: ${UserRole[roleId]}` : "Vai trò không xác định";
};

export const formatTime = (time: number) => {
  const minutes = Math.floor(time / 60);
  const seconds = time % 60;
  return `${minutes}:${seconds < 10 ? "0" : ""}${seconds}`;
};

export const addOneMonthToDate = (date: Date): string => {
  const currentDate = new Date(date);
  currentDate.setMonth(currentDate.getMonth() + 1);
  return currentDate.toLocaleDateString("vi-VN");
};
