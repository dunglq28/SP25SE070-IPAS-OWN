import React, { ReactNode, useEffect, useState } from "react";

import style from "./ManagementLayout.module.scss";
import { HeaderAdmin, SidebarAdmin } from "@/components";
import { Breadcrumb, Flex, Layout, theme } from "antd";
const { Header, Content, Footer, Sider } = Layout;

interface ManagementLayoutProps {
  children: ReactNode;
}

const ManagementLayout: React.FC<ManagementLayoutProps> = ({ children }) => {
  return (
    <Flex>
      <SidebarAdmin />

      <Layout className={style.layout}>
        <HeaderAdmin />
        <Content className={style.contentWrapper}>
          <Flex className={style.content}>
            <Breadcrumb items={[{ title: "Home" }, { title: "Dashboard" }]} />
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
