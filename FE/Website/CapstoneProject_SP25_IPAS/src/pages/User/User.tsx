import { Flex } from "antd";
import style from "./User.module.scss";

function User() {
  return (
    <Flex className={style.container}>
      <h1 style={{ width: "100vh" }}>This is User</h1>
    </Flex>
  );
}

export default User;
