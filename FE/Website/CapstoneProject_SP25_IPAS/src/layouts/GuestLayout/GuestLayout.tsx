import React, { ReactNode } from "react";
import { Layout, Divider } from "antd";

import style from "./GuestLayout.module.scss";
import { Footer, HeaderGuest } from "@/components";

const { Content } = Layout;

interface GuestLayoutProps {
  children: ReactNode;
}

const GuestLayout: React.FC<GuestLayoutProps> = ({ children }) => {
  return (
    <Layout className={style.Wrapper}>
      {/* container */}
      <Layout>
        <Layout>
          <HeaderGuest />
          <Layout>
            <Content className={style.Container}>
              <div className={style.Children}>{children}</div>
            </Content>
            <Divider />
            <Footer />
          </Layout>
        </Layout>
      </Layout>
    </Layout>
  );
};

export default GuestLayout;
