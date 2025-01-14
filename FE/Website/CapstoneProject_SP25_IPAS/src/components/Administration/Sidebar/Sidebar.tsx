import { Avatar, Divider, Flex, Layout, Menu, Tooltip, Typography } from "antd";
import style from "./Sidebar.module.scss";
import { Icons, Images } from "@/assets";
import { useEffect, useRef, useState } from "react";
import {
  DashboardOutlined,
  UserOutlined,
  EnvironmentOutlined,
  BarChartOutlined,
  LogoutOutlined,
} from "@ant-design/icons";
import { useLocation, useNavigate } from "react-router-dom";
import "@/App.css";
import { generateRandomKey } from "@/utils";

const { Sider } = Layout;
const { Text } = Typography;

interface MenuItem {
  key: string;
  label: string;
  icon: React.ReactNode;
  subMenuItems?: SubMenuItem[];
  to?: string;
  category: string;
}

interface SubMenuItem {
  key: string;
  label: string;
  icon: string;
  to?: string;
}

function Sidebar() {
  const [isExpanded, setIsExpanded] = useState(true);
  const navigate = useNavigate();
  const location = useLocation();
  const SIDEBAR_WIDTH_EXPANDED = 280;
  const SIDEBAR_WIDTH_COLLAPSED = 65;

  // Hàm toggle trạng thái Sidebar
  const toggleSidebar = () => {
    setIsExpanded((prev) => !prev);
  };

  const handleNavigation = (to?: string) => {
    if (to) {
      navigate(to);
    }
  };

  const menuItems = [
    {
      key: generateRandomKey(),
      label: "Dashboard",
      icon: <Icons.checkSuccuss />,
      to: "/dashboard",
      category: "Main",
    },
    {
      key: generateRandomKey(),
      label: "User Management",
      icon: <Icons.checkSuccuss />,
      to: "/users",
      category: "Main",
    },
    {
      key: "sub1",
      label: "Farm Management",
      icon: <EnvironmentOutlined />,
      subMenuItems: [
        {
          key: "3",
          label: "Farm Information",
          icon: Images.radius,
          to: "/farmInfo",
        },
        { key: "4", label: "Farm Details", icon: Images.radius, to: "/farmDetail" },
      ],
      category: "Main",
    },
    {
      key: "sub2",
      label: "Process Management",
      icon: <EnvironmentOutlined />,
      subMenuItems: [
        {
          key: "5",
          label: "Process Information",
          icon: Images.radius,
          to: "/processInfo",
        },
        { key: "6", label: "Process Details", icon: Images.radius, to: "/processDetail" },
      ],
      category: "Main",
    },
    {
      key: generateRandomKey(),
      label: "Setting",
      icon: <Icons.checkSuccuss />,
      category: "Settings",
    },
    {
      key: generateRandomKey(),
      label: "Help",
      icon: <Icons.checkSuccuss />,
      category: "Settings",
    },
  ];

  const defaultOpenKeys = menuItems
    .filter((item) => {
      // Nếu có subMenuItems, kiểm tra các mục con
      if (item.subMenuItems) {
        return item.subMenuItems.some((subItem) => subItem.to === location.pathname);
      }
      // Nếu không có subMenuItems, kiểm tra trực tiếp mục cha
      return item.to === location.pathname;
    })
    .map((item) => item.key);

  const defaultKey = menuItems
    .map((item) => {
      // Kiểm tra nếu mục chính hoặc subItem trùng với pathname
      if (item.to === location.pathname) {
        return item.key; // Trả về key của mục chính
      }

      // Kiểm tra trong subMenuItems nếu có
      if (item.subMenuItems) {
        const matchingSubItem = item.subMenuItems.find(
          (subItem) => subItem.to === location.pathname,
        );
        if (matchingSubItem) {
          return matchingSubItem.key; // Trả về key của subItem
        }
      }

      return null; // Nếu không khớp, trả về null
    })
    .find((key) => key !== null);

  const renderMenuItem = (item: MenuItem) => {
    const isActive = item.subMenuItems
      ? item.subMenuItems?.some((subItem) => location.pathname === subItem.to)
      : location.pathname === item.to;

    if (!item.subMenuItems) {
      return (
        <Menu.Item
          key={item.key}
          // icon={item.icon}
          icon={<Flex className={style.MenuIcon}>{item.icon}</Flex>}
          className={`${style.MenuItem} ${isActive ? style.Active : ""}`}
          onClick={() => handleNavigation(item.to)}
          data-menu-key={item.key}
        >
          {item.label}
        </Menu.Item>
      );
    }
    return (
      <Menu.SubMenu
        key={item.key}
        className={`SubMenuItems ${isActive ? "active" : ""}`}
        icon={<Flex className={style.MenuIcon}>{item.icon}</Flex>}
        // icon={item.icon}
        title={isExpanded ? <span className={style.SubMenuItemsTitle}>{item.label}</span> : null}
      >
        {isExpanded && (
          <div
            className={style.SubMenuLine}
            style={{
              height: `${(item.subMenuItems!.length - 1) * 48 + 20}px`,
            }}
          />
        )}

        {item.subMenuItems!.map((subItem) => {
          const isSubItemActive = location.pathname === subItem.to;
          return isExpanded ? (
            <Flex key={subItem.key} className={style.SubMenuItem}>
              <img style={{ width: "24px" }} src={subItem.icon} alt={subItem.label} />
              <Flex
                className={`${style.Item} ${isSubItemActive ? style.Active : ""}`}
                onClick={() => handleNavigation(subItem.to)}
                data-menu-key={subItem.key}
              >
                {subItem.label}
              </Flex>
            </Flex>
          ) : (
            <Menu.Item
              key={subItem.key}
              className={`${style.MenuItem} ${isSubItemActive ? style.Active : ""}`}
              onClick={() => handleNavigation(subItem.to)}
            >
              {subItem.label}
            </Menu.Item>
          );
        })}
      </Menu.SubMenu>
    );
  };

  const renderMenuSection = (category: string) => {
    const sidebarRef = useRef<HTMLDivElement>(null);
    useEffect(() => {
      if (sidebarRef.current && defaultKey) {
        const activeItem = sidebarRef.current.querySelector(`[data-menu-key='${defaultKey}']`);

        if (activeItem) {
          activeItem.scrollIntoView({
            behavior: "smooth",
            block: "nearest",
          });
        }
      }
    }, []);

    return (
      <Flex
        className={`${style.MenuContainer} ${category === "Settings" ? style.MenuMinHeight : ""}`}
        ref={sidebarRef}
      >
        <Flex className={style.WrapperTitle}>
          <Text className={style.Title}>{category}</Text>
        </Flex>
        <Flex className={category === "Settings" ? style.MenuOverflowHidden : ""}>
          <Menu mode="inline" defaultOpenKeys={defaultOpenKeys} className={style.MenuItems}>
            {menuItems
              .filter((item) => item.category === category)
              .map((item) => renderMenuItem(item))}
          </Menu>
        </Flex>
      </Flex>
    );
  };

  return (
    <Sider
      width={isExpanded ? SIDEBAR_WIDTH_EXPANDED : SIDEBAR_WIDTH_COLLAPSED}
      collapsible
      collapsed={!isExpanded}
      trigger={null}
      className={style.SidebarContainer}
    >
      <Flex className={style.Sidebar}>
        {/* Header */}
        <Flex>
          <Flex
            className={style.Logo}
            style={{
              justifyContent: !isExpanded ? "center" : undefined,
            }}
          >
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

        {/* Main Menu */}
        {renderMenuSection("Main")}

        <Flex className={style.WrapperDivider}>
          <Divider className={style.Divider} />
        </Flex>

        {/* Settings Menu */}
        {renderMenuSection("Settings")}

        <Flex
          className={style.Profile}
          style={{
            justifyContent: !isExpanded ? "center" : undefined,
          }}
        >
          <Tooltip title={!isExpanded ? "Logout Account" : ""} placement="right">
            <Icons.logout className={style.LogoutIcon} />
          </Tooltip>
          {isExpanded && <Text className={style.LogoutText}>Logout Account</Text>}
        </Flex>
      </Flex>
    </Sider>
  );
}

export default Sidebar;
