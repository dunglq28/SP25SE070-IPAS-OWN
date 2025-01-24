import React, { ReactNode } from "react";

import style from "./ManagementLayout.module.scss";
import { HeaderAdmin, SidebarAdmin } from "@/components";
import { Breadcrumb, Flex, Layout } from "antd";
import { Link, useLocation } from "react-router-dom";
import { isValidBreadcrumb } from "@/utils";
const { Content, Footer } = Layout;

interface ManagementLayoutProps {
  children: ReactNode;
}

const ManagementLayout: React.FC<ManagementLayoutProps> = ({ children }) => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter((x) => x);

  const breadcrumbItems = pathnames
    .filter((path) => !isValidBreadcrumb(path))
    .map((path, index, filteredPaths) => {
      const pathTo = `/${pathnames.slice(0, index + 1).join("/")}`;
      const title = path.charAt(0).toUpperCase() + path.slice(1).toLowerCase();

      const isLastItem = index === filteredPaths.length - 1;

      return isLastItem
        ? { title: <span className={style.active}>{title}</span> }
        : { title: <Link to={pathTo}>{title}</Link> };
    });

  return (
    <Flex>
      <SidebarAdmin />
      <Layout className={style.layout}>
        <HeaderAdmin />
        <Content className={style.contentWrapper}>
          <Flex className={style.content}>
            <Breadcrumb items={breadcrumbItems} />
            {children}
          </Flex>
        </Content>
        <Footer className={style.footer}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Flex>
  );
};

export default ManagementLayout;
