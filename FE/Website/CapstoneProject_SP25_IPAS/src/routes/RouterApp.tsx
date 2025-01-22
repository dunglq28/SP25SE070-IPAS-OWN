import { Dashboard, FarmPicker, Login, PlantList, User } from "@/pages";

import { FarmPickerLayout, GuestLayout, HeaderOnly, ManagementLayout } from "@/layouts";
import { PATHS } from "./Paths";
import Landing from "@/pages/LandingPage/Landing";

interface RouteItem {
  path: string;
  component: () => JSX.Element;
  layout?: React.ComponentType<any> | null;
}

export const publicRoutes: RouteItem[] = [
  { path: PATHS.AUTH.LOGIN, component: Landing, layout: GuestLayout },
  { path: PATHS.AUTH.LOGIN, component: Login, layout: GuestLayout },
  { path: PATHS.FARM_PICKER, component: FarmPicker, layout: FarmPickerLayout },
  { path: PATHS.DASHBOARD, component: Dashboard, layout: ManagementLayout },
  { path: PATHS.USER.USER_LIST, component: User, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLANT_LIST, component: PlantList, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLANT_DETAIL, component: Dashboard, layout: ManagementLayout },
];

export const privateRoutes: RouteItem[] = [];
