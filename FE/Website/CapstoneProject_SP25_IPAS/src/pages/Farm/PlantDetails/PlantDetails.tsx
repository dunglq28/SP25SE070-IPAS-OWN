import { Flex, Tabs, TabsProps } from "antd";
import style from "./PlantDetail.module.scss";
import { useStyle } from "@/hooks";
import { Icons } from "@/assets";
import { Tooltip } from "@/components";
import { useNavigate } from "react-router-dom";
import { PATHS } from "@/routes";
import PlantDetail from "./PlantDetail";
const TabPane = Tabs.TabPane;

function PlantDetails() {
  const navigate = useNavigate();
  const { styles } = useStyle();

  const items: TabsProps["items"] = [
    {
      key: "1",
      icon: <Icons.overview className={style.iconTab} />,
      label: <label className={style.titleTab}>Overview</label>,
      children: "Content of Tab Pane 1",
    },
    {
      key: "2",
      icon: <Icons.detail className={style.iconTab} />,
      label: <label className={style.titleTab}>Detail</label>,
      children: <PlantDetail />,
    },
    {
      key: "3",
      icon: <Icons.criteria className={style.iconTab} />,
      label: <label className={style.titleTab}>Criteria</label>,
      children: "Content of Tab Pane 3",
    },
    {
      key: "4",
      icon: <Icons.history className={style.iconTab} />,
      label: <label className={style.titleTab}>Growth History</label>,
      children: "Content of Tab Pane 3",
    },
  ];

  const onChange = (key: string) => {
    console.log(key);
  };

  return (
    <Flex className={style.container}>
      <Tabs
        className={`${style.containerWrapper} ${styles.customTab}`}
        defaultActiveKey="2"
        items={items}
        onChange={onChange}
        tabBarExtraContent={{
          left: (
            <Flex className={style.extraContent}>
              <Tooltip
                title="Back to List"
                children={
                  <Icons.back
                    className={style.backIcon}
                    onClick={() => navigate(PATHS.FARM.FARM_PLANT_LIST)}
                  />
                }
              />
            </Flex>
          ),
        }}
      />
    </Flex>
  );
}

export default PlantDetails;
