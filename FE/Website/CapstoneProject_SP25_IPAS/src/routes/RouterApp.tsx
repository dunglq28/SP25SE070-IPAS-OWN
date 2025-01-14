import { Dashboard, Login, User } from "@/pages";

import { GuestLayout, HeaderOnly, ManagementLayout } from "@/layouts";

interface RouteItem {
  path: string;
  component: () => JSX.Element;
  layout?: React.ComponentType<any> | null;
}

export const publicRoutes: RouteItem[] = [
  { path: "/", component: Login, layout: GuestLayout },
  { path: "/dashboard", component: Dashboard, layout: ManagementLayout },
  { path: "/users", component: User, layout: ManagementLayout },
  { path: "/farmInfo", component: User, layout: ManagementLayout },
  { path: "/farmDetail", component: User, layout: ManagementLayout },
  { path: "/processInfo", component: User, layout: ManagementLayout },
  { path: "/processDetail", component: User, layout: ManagementLayout },

];

export const privateRoutes: RouteItem[] = [];
