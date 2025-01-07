import { Flex } from "antd";
import style from "./Dashboard.module.scss";

function Dashboard() {
  return (
    <Flex className={style.container}>
      <h1 style={{width: "100vh"}}>This is Dashboard</h1>
    </Flex>
  );
}

export default Dashboard;
