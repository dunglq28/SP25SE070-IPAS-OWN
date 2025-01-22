import React, { ReactNode, useEffect, useState } from "react";

import style from "./ManagementLayout.module.scss";
import { HeaderAdmin, SidebarAdmin } from "@/components";
import { Breadcrumb, Flex, Layout, theme } from "antd";
import { Link, useLocation } from "react-router-dom";
const { Header, Content, Footer, Sider } = Layout;

interface ManagementLayoutProps {
  children: ReactNode;
}

const ManagementLayout: React.FC<ManagementLayoutProps> = ({ children }) => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter((x) => x);
  const breadcrumbItems = pathnames.map((path, index) => {
    const pathTo = `/${pathnames.slice(0, index + 1).join("/")}`;

    const title = path.charAt(0).toUpperCase() + path.slice(1).toLowerCase();

    return index === pathnames.length - 1
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
