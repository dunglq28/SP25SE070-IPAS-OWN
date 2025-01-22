import React, { ReactNode } from "react";

import style from "./FarmPickerLayout.module.scss";
import { Flex, Layout } from "antd";
import { HeaderAdmin, SidebarAdmin } from "@/components";
const { Header, Content, Footer } = Layout;

interface FarmPickerLayoutProps {
  children: ReactNode;
}

const FarmPickerLayout: React.FC<FarmPickerLayoutProps> = ({ children }) => {
  return (
    <Flex>
      <SidebarAdmin isDefault={true} />
      <Layout className={style.layout}>
        <HeaderAdmin isDefault={true} />
        <Content className={style.contentWrapper}>
          <Flex className={style.content}>{children}</Flex>
        </Content>
        <Footer className={style.footer}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Flex>
  );
};

export default FarmPickerLayout;
