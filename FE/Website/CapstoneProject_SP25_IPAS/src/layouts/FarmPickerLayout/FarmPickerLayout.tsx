import React, { ReactNode } from "react";

import style from "./FarmPickerLayout.module.scss";
import { Flex, Layout } from "antd";
import { HeaderAdmin, Loading, SidebarAdmin } from "@/components";
import { useAuthRedirect, useToastMessage } from "@/hooks";
const { Content, Footer } = Layout;

interface FarmPickerLayoutProps {
  children: ReactNode;
}

const FarmPickerLayout: React.FC<FarmPickerLayoutProps> = ({ children }) => {
  const isAuthChecked = useAuthRedirect();
  useToastMessage();

  if (!isAuthChecked) {
    return <Loading />;
  }

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
