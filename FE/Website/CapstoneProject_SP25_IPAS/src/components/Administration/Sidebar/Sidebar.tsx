import { Avatar, Button, Divider, Flex, Layout, Menu, Typography } from "antd";
import style from "./Sidebar.module.scss";
import { Icons, Images } from "@/assets";
import { useState } from "react";

const { Sider } = Layout;
const { Text } = Typography;

function Sidebar() {
  const [isExpanded, setIsExpanded] = useState(true);

  // Hàm toggle trạng thái Sidebar
  const toggleSidebar = () => {
    setIsExpanded((prev) => !prev);
  };

  return (
    <Sider width={isExpanded ? 250 : 65} collapsible collapsed={!isExpanded} trigger={null}>
      <Flex className={style.Sidebar}>
        <Flex>
          <Flex className={style.Logo}>
            <Avatar src={Images.react} className={style.Avatar} />
            {isExpanded && <Text className={style.LogoText}>Tan Trieu Pomelo</Text>}
          </Flex>
          <Icons.arrowForward
            className={style.ArrowSidebar}
            onClick={toggleSidebar}
            style={{
              transform: `rotate(${isExpanded ? 180 : 0}deg)`,
              color: "#fff",
            }}
          />
        </Flex>

        <Flex className={style.WrapperDivider}>
          <Divider className={style.Divider} />
        </Flex>

        <Flex className={style.MenuItems}>
          <Flex className={style.WrapperTitle}>
            <Text className={style.Title}>Main</Text>
          </Flex>

          <Flex>
            <Menu mode="vertical" className={style.MenuItems}>
              {/* Example menu items */}
              <Menu.Item key="1" icon={<Icons.checkSuccuss />} className={style.MenuItem}>
                Crop Care Management
              </Menu.Item>
              <Menu.Item key="2" icon={<Icons.closeFail />} className={style.MenuItem}>
                Menu Item 2
              </Menu.Item>
              {/* Add more menu items here */}
            </Menu>
          </Flex>
        </Flex>

        <Flex className={style.WrapperDivider}>
          <Divider className={style.Divider} />
        </Flex>

        <Flex className={style.MenuItems}>
          <Flex className={style.WrapperTitle}>
            <Text className={style.Title}>Settings</Text>
          </Flex>

          <Flex>
            <Menu mode="vertical" className={style.MenuItems}>
              {/* Example menu items */}
              <Menu.Item key="1" icon={<Icons.checkSuccuss />} className={style.MenuItem}>
                Crop Care Management
              </Menu.Item>
              <Menu.Item key="2" icon={<Icons.closeFail />} className={style.MenuItem}>
                Menu Item 2
              </Menu.Item>
              {/* Add more menu items here */}
            </Menu>
          </Flex>
        </Flex>

        <Flex className={style.Profile}>
          <Icons.delete />
          {isExpanded && <Text className={style.LogoutText}>Logout</Text>}
        </Flex>
      </Flex>
    </Sider>
  );
}

export default Sidebar;
