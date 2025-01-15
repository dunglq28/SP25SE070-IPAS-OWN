import { Avatar, Divider, Flex, Layout, Menu, Tooltip, Typography } from "antd";
import style from "./Sidebar.module.scss";
import { Icons, Images } from "@/assets";
import { useEffect, useRef, useState } from "react";
import { EnvironmentOutlined } from "@ant-design/icons";
import { matchPath, useLocation, useNavigate } from "react-router-dom";
import "@/App.css";
import { PATHS } from "@/routes";
import { useSidebarStore } from "@/stores";

const { Sider } = Layout;
const { Text } = Typography;

interface MenuItem {
  key: string;
  label: string;
  icon: React.ReactNode;
  subMenuItems?: SubMenuItem[];
  to?: string;
  activePaths: string[];
  category: string;
}

interface SubMenuItem {
  key: string;
  label: string;
  icon: string;
  to?: string;
  activePaths: string[];
}

interface ActiveMenu {
  parentKey: string | null;
  subItemKey: string | null;
}

function Sidebar() {
  const navigate = useNavigate();
  const location = useLocation();
  const SIDEBAR_WIDTH_EXPANDED = 280;
  const SIDEBAR_WIDTH_COLLAPSED = 65;
  const [activeMenu, setActiveMenu] = useState<ActiveMenu>({
    parentKey: null,
    subItemKey: null,
  });

  const { isExpanded, toggleSidebar } = useSidebarStore();

  const handleNavigation = (to?: string) => {
    if (to) {
      navigate(to);
    }
  };

  const menuItems = [
    {
      key: "Dashboard",
      label: "Dashboard",
      icon: <Icons.checkSuccuss />,
      to: PATHS.DASHBOARD,
      activePaths: [PATHS.DASHBOARD],
      category: "Main",
    },
    {
      key: "User Management",
      label: "User Management",
      icon: <Icons.checkSuccuss />,
      to: PATHS.USER.USER_LIST,
      activePaths: [PATHS.USER.USER_LIST, PATHS.USER.USER_DETAIL],
      category: "Main",
    },
    {
      key: "Farm Management",
      label: "Farm Management",
      icon: <EnvironmentOutlined />,
      activePaths: [
        PATHS.FARM.FARM_LIST,
        PATHS.FARM.FARM_DETAIL,
        PATHS.FARM.FARM_PLOT_LIST,
        PATHS.FARM.FARM_PLOT_CREATE,
      ],
      subMenuItems: [
        {
          key: "Farm List",
          label: "Farm List",
          icon: Images.radius,
          to: PATHS.FARM.FARM_LIST,
          activePaths: [PATHS.FARM.FARM_LIST, PATHS.FARM.FARM_DETAIL],
        },
        {
          key: "Land Plot List",
          label: "Land Plot List",
          icon: Images.radius,
          to: PATHS.FARM.FARM_PLOT_LIST,
          activePaths: [PATHS.FARM.FARM_PLOT_LIST, PATHS.FARM.FARM_PLOT_CREATE],
        },
      ],
      category: "Main",
    },
    {
      key: "Process Management",
      label: "Process Management",
      icon: <EnvironmentOutlined />,
      activePaths: [PATHS.PROCESS.PROCESS_LIST, PATHS.PROCESS.PROCESS_DETAIL],
      subMenuItems: [
        {
          key: "Process List",
          label: "Process List",
          icon: Images.radius,
          to: PATHS.PROCESS.PROCESS_LIST,
          activePaths: [PATHS.PROCESS.PROCESS_LIST, PATHS.PROCESS.PROCESS_DETAIL],
        },
      ],
      category: "Main",
    },
    {
      key: "Setting",
      label: "Setting",
      icon: <Icons.checkSuccuss />,
      activePaths: [""],
      category: "Settings",
    },
    {
      key: "Help",
      label: "Help",
      icon: <Icons.checkSuccuss />,
      activePaths: [""],
      category: "Settings",
    },
  ];

  useEffect(() => {
    const findMatchingPath = (paths: string[], pathname: string) => {
      return paths.some((path) => {
        if (path.includes(":id")) {
          return matchPath(path, pathname);
        }
        return pathname === path;
      });
    };

    const currentItem = menuItems.find(
      (menuItem) =>
        (menuItem.activePaths && findMatchingPath(menuItem.activePaths, location.pathname)) ||
        location.pathname === menuItem.to,
    );

    if (currentItem) {
      let matchingSubMenu = null;

      // Tìm submenu item phù hợp
      matchingSubMenu =
        currentItem.subMenuItems?.find((subItem) =>
          subItem.activePaths.some((path) => location.pathname === path),
        ) ||
        currentItem.subMenuItems?.find((subItem) =>
          findMatchingPath(subItem.activePaths, location.pathname),
        );

      setActiveMenu({
        parentKey: currentItem.key || null,
        subItemKey: matchingSubMenu ? matchingSubMenu.key : null,
      });
    }
  }, [location.pathname]);

  const defaultOpenKeys = menuItems
    .filter((item) => {
      // Kiểm tra nếu item có activePaths và xem location.pathname có chứa bất kỳ giá trị nào trong mảng activePaths
      return item.activePaths && item.activePaths.some((path) => location.pathname.includes(path));
    })
    .map((item) => item.key)
    .filter((key): key is string => key !== undefined);

  const defaultKey = menuItems
    .map((item) => {
      // Kiểm tra nếu mục chính không có submenu và location.pathname có trong activePaths của item
      if (item.activePaths && !item.subMenuItems && item.activePaths.includes(location.pathname)) {
        return item.key; // Trả về key của mục chính
      }

      // Kiểm tra nếu mục chính có submenu và location.pathname có trong activePaths của subMenuItems
      if (item.subMenuItems) {
        const matchingSubItem = item.subMenuItems.find((subItem) =>
          subItem.activePaths.includes(location.pathname),
        );
        if (matchingSubItem) {
          return matchingSubItem.key; // Trả về key của subItem
        }
      }

      return null; // Nếu không khớp, trả về null
    })
    .find((key) => key !== null);

  const renderMenuItem = (item: MenuItem) => {
    const isMainMenuActive = item.key === activeMenu.parentKey;

    if (!item.subMenuItems) {
      return (
        <Menu.Item
          key={item.key}
          icon={<Flex className={style.MenuIcon}>{item.icon}</Flex>}
          className={`${style.MenuItem} ${isMainMenuActive ? style.Active : ""}`}
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
        className={`SubMenuItems ${isMainMenuActive ? "active" : ""}`}
        icon={<Flex className={style.MenuIcon}>{item.icon}</Flex>}
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
          const isSubItemActive = subItem.key === activeMenu.subItemKey;
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
