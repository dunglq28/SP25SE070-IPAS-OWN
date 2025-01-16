import { Dashboard, Login, User } from "@/pages";

import { GuestLayout, HeaderOnly, ManagementLayout } from "@/layouts";
import { PATHS } from "./Paths";

interface RouteItem {
  path: string;
  component: () => JSX.Element;
  layout?: React.ComponentType<any> | null;
}

export const publicRoutes: RouteItem[] = [
  { path: PATHS.AUTH.LOGIN, component: Login, layout: GuestLayout },
  { path: PATHS.DASHBOARD, component: Dashboard, layout: ManagementLayout },
  { path: PATHS.USER.USER_LIST, component: User, layout: ManagementLayout },
  { path: PATHS.USER.USER_DETAIL, component: User, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_LIST, component: User, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_DETAIL, component: User, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLOT_LIST, component: Dashboard, layout: ManagementLayout },
  { path: PATHS.FARM.FARM_PLOT_CREATE, component: Dashboard, layout: ManagementLayout },
  { path: PATHS.PROCESS.PROCESS_LIST, component: User, layout: ManagementLayout },
  { path: PATHS.PROCESS.PROCESS_DETAIL, component: User, layout: ManagementLayout },
];

export const privateRoutes: RouteItem[] = [];
