import React, { ReactNode } from "react";
import { Layout } from "antd";

import style from "./GuestLayout.module.scss";
import { Footer, HeaderGuest } from "@/components";
import { useAuthRedirect, useToastFromLocalStorage, useToastMessage } from "@/hooks";

const { Content } = Layout;

interface GuestLayoutProps {
  children: ReactNode;
}

const GuestLayout: React.FC<GuestLayoutProps> = ({ children }) => {
  // useAuthRedirect();
  useToastMessage();
  useToastFromLocalStorage();
  return (
    <Layout>
      {/* container */}
      <Layout>
        <Layout>
          <HeaderGuest />
          <Layout>
            <Content className={style.Container}>
              <div className={style.Children}>{children}</div>
            </Content>
            <Footer />
          </Layout>
        </Layout>
      </Layout>
    </Layout>
  );
};

export default GuestLayout;
