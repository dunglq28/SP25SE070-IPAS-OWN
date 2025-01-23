
export const PATHS = {
  // Authentication
  AUTH: {
    LANDING: "/",
    LOGIN: "/auth",
    FORGOT_PASSWORD: "/forgot-password",
    FORGOT_PASSWORD_OTP: "/forgot-password/otp",
    SIGN_UP_OTP: "/sign-up/otp",
  },

  // Dashboard
  DASHBOARD: "/dashboard",

  FARM_PICKER: "/farm-picker",

  // User Management
  USER: {
    USER_LIST: "/users",
    USER_DETAIL: "/users/:id",
  },

  // Farm Management
  FARM: {
    FARM_LIST: "/farms",
    FARM_DETAIL: "/farms/:id",
    FARM_PLANT_LIST: "/farms/plants",
    FARM_PLANT_DETAIL: "/farms/plants/:id/detail",
    FARM_PLOT_LIST: "/farms/land-plots",
    FARM_PLOT_CREATE: "/farms/land-plot/create",
  },

  // Process Management
  PROCESS: {
    PROCESS_LIST: "/processes",
    PROCESS_DETAIL: "/processes/:id",
  },
};
