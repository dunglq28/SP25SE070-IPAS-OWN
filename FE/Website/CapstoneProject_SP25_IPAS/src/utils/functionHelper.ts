import moment from "moment";
import { UserRole } from "@/constants/Enum";
import { camelCase, kebabCase } from "change-case";

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

export const getCurrentDate = (): string => {
  return moment().format("dddd, DD/MM/YYYY");
};

export const formatDate = (date: Date): string => {
  return moment(date).format("DD/MM/YYYY");
};

export const formatDateAndTime = (date: Date): string => {
  return moment(date).format("DD/MM/YYYY HH:mm:ss");
};

export const getRoleName = (roleId: number): string => {
  if (roleId === UserRole.Admin) {
    return "Quản trị viên";
  } else if (roleId === UserRole.BrandManager) {
    return "Quản lý thương hiệu";
  } else if (roleId === UserRole.BranchManager) {
    return "Quản lý chi nhánh";
  }
  return UserRole[roleId] ? `Vai trò: ${UserRole[roleId]}` : "Vai trò không xác định";
};

export const getGender = (gender: string): string => {
  if (gender === "Male") {
    return "Nam";
  } else if (gender === "Female") {
    return "Nữ";
  }
  return gender;
};

export const formatTime = (time: number) => {
  const minutes = Math.floor(time / 60);
  const seconds = time % 60;
  return `${minutes}:${seconds < 10 ? "0" : ""}${seconds}`;
};

export const translateDemographics = (demographics: string): string => {
  const [gender, time] = demographics.split(", ") as [string, string];

  const genderMap: { [key: string]: string } = {
    Male: "Nam",
    Female: "Nữ",
  };

  const timeMap: { [key: string]: string } = {
    Morning: "Buổi Sáng",
    Afternoon: "Buổi Trưa",
    Evening: "Buổi Chiều",
  };

  return `${genderMap[gender] || "Không xác định"}, ${timeMap[time] || "Không xác định"}`;
};

export const addOneMonthToDate = (date: Date): string => {
  const currentDate = new Date(date);
  currentDate.setMonth(currentDate.getMonth() + 1);
  return currentDate.toLocaleDateString("vi-VN");
};
