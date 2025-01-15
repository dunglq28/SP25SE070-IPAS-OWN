import React, { ReactNode, useEffect, useState } from "react";

import style from "./ManagementLayout.module.scss";
import { HeaderAdmin, SidebarAdmin } from "@/components";
import { Breadcrumb, Flex, Layout, theme } from "antd";
const { Header, Content, Footer, Sider } = Layout;

interface ManagementLayoutProps {
  children: ReactNode;
}

const ManagementLayout: React.FC<ManagementLayoutProps> = ({ children }) => {
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  return (
    <Flex>
      <SidebarAdmin />

      <Layout style={{ flex: 1 }}>
        {/* <Header style={{ padding: 0, background: colorBgContainer }} /> */}
        <HeaderAdmin />
        <Content style={{ margin: "0 16px" }}>
          <Breadcrumb items={[{ title: "Home" }, { title: "Dashboard" }]} />;
          <div
            style={{
              padding: 24,
              minHeight: 360,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            Bill is a cat.
          </div>
          {/* {children} */}
        </Content>
        <Footer style={{ textAlign: "center" }}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Flex>
  );
};

export default ManagementLayout;
