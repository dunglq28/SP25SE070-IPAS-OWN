export const PATHS = {
  // Authentication
  AUTH: {
    LOGIN: "/",
  },

  // Dashboard
  DASHBOARD: "/dashboard",

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
    FARM_PLOT_LIST: "/farms/land-plots",
    FARM_PLOT_CREATE: "/farms/land-plot/create",
  },

  // Process Management
  PROCESS: {
    PROCESS_LIST: "/processes",
    PROCESS_DETAIL: "/processes/:id",
  },
};
