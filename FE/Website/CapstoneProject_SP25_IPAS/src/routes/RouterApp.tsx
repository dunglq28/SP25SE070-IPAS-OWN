import { Authentication, Dashboard, PlantList, User, FarmPicker,ForgetPassword, Landing, OTP } from "@/pages";
import { FarmPickerLayout, GuestLayout, HeaderOnly, ManagementLayout } from "@/layouts";
import { PATHS } from "./Paths";

interface RouteItem {
  path: string;
  component: () => JSX.Element;
  layout?: React.ComponentType<any> | null;
}

export const publicRoutes: RouteItem[] = [
  { path: PATHS.AUTH.LANDING, component: Landing, layout: GuestLayout },
  { path: PATHS.FARM_PICKER, component: FarmPicker, layout: FarmPickerLayout },
  { path: PATHS.AUTH.LOGIN, component: Authentication, layout: null },
  { path: PATHS.AUTH.FORGOT_PASSWORD, component: ForgetPassword, layout: null },
  { path: PATHS.AUTH.OTP, component: OTP, layout: null },
  { path: PATHS.DASHBOARD, component: Dashboard, layout: ManagementLayout },
  { path: PATHS.USER.USER_LIST, component: User, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLANT_LIST, component: PlantList, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLANT_DETAIL, component: Dashboard, layout: ManagementLayout },
];

export const privateRoutes: RouteItem[] = [];
