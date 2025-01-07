import { Dashboard, Login } from "@/pages";

import { GuestLayout, HeaderOnly, ManagementLayout } from "@/layouts";

interface RouteItem {
  path: string;
  component: () => JSX.Element;
  layout?: React.ComponentType<any> | null;
}

export const publicRoutes: RouteItem[] = [
  { path: "/", component: Login, layout: GuestLayout },
  { path: "/Dashboard", component: Dashboard, layout: ManagementLayout },
];

export const privateRoutes: RouteItem[] = [];
