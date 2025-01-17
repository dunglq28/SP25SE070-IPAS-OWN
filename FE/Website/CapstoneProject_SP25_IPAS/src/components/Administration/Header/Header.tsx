import { Avatar, Button, Col, Flex, Popover, Row, Typography } from "antd";
import { UserOutlined } from "@ant-design/icons";
const { Text } = Typography;

import style from "./Header.module.scss";
import { getCurrentDate, getRoleName } from "@/utils";
import { Icons, Images } from "@/assets";
import { useNavigate } from "react-router-dom";

function Header() {
  const menuItems = [
    { label: "Hồ sơ cá nhân", path: "/profile" },
    { label: "Cài đặt tài khoản", path: "/settings" },
  ];

  const navigate = useNavigate();

  const handleClick = (path: string) => {
    navigate(path);
  };

  const profileContent = (
    <div className={style.popupContainer}>
      <Flex className={style.popupNav}>
        {menuItems.map((item, index) => (
          <Flex key={index} onClick={() => handleClick(item.path)} className={style.popupSubNav}>
            <Text>{item.label}</Text>
          </Flex>
        ))}
      </Flex>
    </div>
  );

  const notificationContent = (
    <div>
      <h4>Notifications</h4>
      <p>No new notifications</p>
    </div>
  );

  const notifications = [
    { icon: <Icons.language />, content: notificationContent },
    { icon: <Icons.regBell />, content: notificationContent },
  ];

  return (
    <Flex className={style.header}>
      <Flex className={style.content}>
        <Flex className={style.leftSection}>
          <Text className={style.welcomeMessage}>Welcome back, dsd!</Text>
          <Flex className={style.dateWrapper}>
            <Icons.calendar className={style.dateIcon} />
            <Text className={style.dateText}>{getCurrentDate()}</Text>
          </Flex>
        </Flex>
        <Flex className={style.rightSection}>
          <Flex className={style.notificationWrapper}>
            {notifications.map((noti, index) => (
              <Popover key={index} content={noti.content} trigger="click" placement="bottomRight">
                <Button className={style.notificationButton}>{noti.icon}</Button>
              </Popover>
            ))}
          </Flex>
          <Popover content={profileContent} trigger="click" placement="bottomRight">
            <Flex className={style.profileContainer}>
              {/* <Avatar shape="square" size={50} icon={<UserOutlined />} /> */}
              <Avatar size={50} shape="square" src={<img src={Images.avatar} alt="avatar" />} />
              <Flex className={style.profileInfo}>
                <Text className={style.profileName}>Admin</Text>
                <Text className={style.profileRole}>{getRoleName(Number(1))}</Text>
              </Flex>
              <Icons.arrowDropDownLine className={style.dropdownIcon} />
            </Flex>
          </Popover>
        </Flex>
      </Flex>
    </Flex>
  );
}

export default Header;
