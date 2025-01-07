import React, { ReactNode } from "react";
import { Layout } from "antd";
import { HeaderAdmin } from "@/components";
import style from "./HeaderOnly.module.scss";

const { Content } = Layout;

interface HeaderOnlyProps {
  children: ReactNode;
}

const HeaderOnly: React.FC<HeaderOnlyProps> = ({ children }) => {
  return (
    <Layout className={style.Wrapper}>
      <Layout>
        <HeaderAdmin />
        <Layout style={{ padding: "0 50px" }}>
          <Content className={style.Container}>
            <div className={style.Children}>{children}</div>
          </Content>
        </Layout>
      </Layout>
    </Layout>
  );
};

export default HeaderOnly;
