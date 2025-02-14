import {
  Authentication,
  Dashboard,
  PlantList,
  User,
  FarmPicker,
  ForgetPassword,
  Landing,
  OTP,
  PlantDetails,
  Weather,
  ResetPassword,
} from "@/pages";
import { EmptyLayout, FarmPickerLayout, GuestLayout, ManagementLayout } from "@/layouts";
import { PATHS } from "./Paths";

interface RouteItem {
  path: string;
  component: () => JSX.Element;
  layout?: React.ComponentType<any> | null;
  props?: Record<string, any>;
}

export const publicRoutes: RouteItem[] = [
  { path: PATHS.AUTH.LANDING, component: Landing, layout: GuestLayout },
  { path: PATHS.FARM_PICKER, component: FarmPicker, layout: FarmPickerLayout },
  { path: PATHS.AUTH.LOGIN, component: Authentication, layout: EmptyLayout },
  { path: PATHS.AUTH.FORGOT_PASSWORD, component: ForgetPassword, layout: EmptyLayout },
  {
    path: PATHS.AUTH.FORGOT_PASSWORD_OTP,
    component: OTP,
    layout: EmptyLayout,
    props: { type: "reset" },
  },
  {
    path: PATHS.AUTH.FORGOT_PASSWORD_RESET,
    component: ResetPassword,
    layout: EmptyLayout,
  },
  { path: PATHS.AUTH.SIGN_UP_OTP, component: OTP, layout: EmptyLayout, props: { type: "sign-up" } },
  { path: PATHS.DASHBOARD, component: Dashboard, layout: ManagementLayout },
  { path: PATHS.USER.USER_LIST, component: User, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLANT_LIST, component: PlantList, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLANT_DETAIL, component: PlantDetails, layout: ManagementLayout },
  { path: PATHS.WEATHER.WEATHER, component: Weather, layout: ManagementLayout },
];

export const privateRoutes: RouteItem[] = [];
