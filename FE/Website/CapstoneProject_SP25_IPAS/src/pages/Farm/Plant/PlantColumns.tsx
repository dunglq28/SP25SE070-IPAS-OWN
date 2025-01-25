import { TableColumn } from "@/types";
import { GetPlant } from "@/payloads";
import style from "./PlantList.module.scss";

export const plantColumns: TableColumn<GetPlant>[] = [
  {
    header: "Họ và tên",
    field: "fullname",
    accessor: (plant) => plant.fullname,
    width: 150,
  },
  {
    header: "Tên tài khoản",
    field: "userName",
    accessor: (plant) => <div className={style.tableText}>{plant.userName}</div>,
    width: 300,
  },
  {
    header: "Số điện thoại",
    field: "phone",
    accessor: (plant) =>  <div className={style.tableText}>{plant.phone}</div>,
    width: 150,
  },
  {
    header: "Số điện thoại",
    field: "phone",
    accessor: (plant) =>  <div className={style.tableText}>{plant.phone}</div>,
    width: 150,
  },
  {
    header: "Số điện thoại",
    field: "phone",
    accessor: (plant) =>  <div className={style.tableText}>{plant.phone}</div>,
    width: 150,
  },
  {
    header: "Số điện thoại",
    field: "phone",
    accessor: (plant) =>  <div className={style.tableText}>{plant.phone}</div>,
    width: 150,
  },
  {
    header: "Số điện thoại",
    field: "phone",
    accessor: (plant) =>  <div className={style.tableText}>{plant.phone}</div>,
    width: 150,
  },
  {
    header: "Vai trò",
    field: "roleId",
    accessor: (plant) => plant.roleId,
    width: 100,
  },
  {
    header: "Đang hoạt động",
    field: "isActive",
    accessor: (plant) => (plant.isActive ? "Có" : "Không"),
    width: 170,
  },
];
