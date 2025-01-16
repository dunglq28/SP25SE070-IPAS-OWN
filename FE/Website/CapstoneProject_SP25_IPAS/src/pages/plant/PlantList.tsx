import { Flex } from "antd";
import style from "./PlantList.module.scss";

function PlantList() {
  return (
    <Flex className={style.container}>
      <h1 style={{ width: "100vh" }}>This is Plant</h1>
    </Flex>
  );
}

export default PlantList;
