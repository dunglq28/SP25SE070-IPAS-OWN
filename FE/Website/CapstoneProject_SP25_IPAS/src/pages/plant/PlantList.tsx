import { Flex } from "antd";
import style from "./PlantList.module.scss";
import { Table } from "@/components";
import { TableColumn } from "@/types";
import { GetPlant } from "@/payloads";
import { Icons } from "@/assets";
import { useSort } from "@/hooks";
import { useEffect } from "react";

function PlantList() {
  const { sortField, sortDirection, rotation, handleSortChange } = useSort({});

  const fakePlants: GetPlant[] = [
    {
      userCode: "PL001",
      userId: 1,
      fullname: "Nguyễn Văn A",
      userName:
        "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
      phone: "0123456789",
      roleId: 1,
      isActive: true,
      status: 1, // 1 có thể là trạng thái 'active'
    },
    {
      userCode: "PL002",
      userId: 2,
      fullname: "Trần Thị B",
      userName: "tranthib",
      phone: "0987654321",
      roleId: 2,
      isActive: false,
      status: 0, // 0 có thể là trạng thái 'inactive'
    },
    {
      userCode: "PL003",
      userId: 3,
      fullname: "Lê Minh C",
      userName: "leminhc",
      phone: "0333333333",
      roleId: 3,
      isActive: true,
      status: 1,
    },
    {
      userCode: "PL004",
      userId: 4,
      fullname: "Phan Thị D",
      userName: "phanthid",
      phone: "0444444444",
      roleId: 2,
      isActive: false,
      status: 0,
    },
    {
      userCode: "PL005",
      userId: 5,
      fullname: "Lê Văn E",
      userName: "levane",
      phone: "0555555555",
      roleId: 1,
      isActive: true,
      status: 1,
    },
    {
      userCode: "PL006",
      userId: 6,
      fullname: "Nguyễn Thị F",
      userName: "nguyenf",
      phone: "0666666666",
      roleId: 3,
      isActive: false,
      status: 0,
    },
    {
      userCode: "PL007",
      userId: 7,
      fullname: "Trần Minh G",
      userName: "tranming",
      phone: "0777777777",
      roleId: 2,
      isActive: true,
      status: 1,
    },
    {
      userCode: "PL008",
      userId: 8,
      fullname: "Phan Thị H",
      userName: "phanthih",
      phone: "0888888888",
      roleId: 1,
      isActive: false,
      status: 0,
    },
    {
      userCode: "PL009",
      userId: 9,
      fullname: "Nguyễn Minh I",
      userName: "nguyenmi",
      phone: "0999999999",
      roleId: 2,
      isActive: true,
      status: 1,
    },
    {
      userCode: "PL010",
      userId: 10,
      fullname: "Lê Thị J",
      userName: "lethij",
      phone: "1010101010",
      roleId: 3,
      isActive: false,
      status: 0,
    },
  ];

  const plantColumns: TableColumn<GetPlant>[] = [
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
      accessor: (plant) => plant.phone,
      width: 150,
    },
    {
      header: "Số điện thoại",
      field: "phone",
      accessor: (plant) => plant.phone,
      width: 150,
    },
    {
      header: "Số điện thoại",
      field: "phone",
      accessor: (plant) => plant.phone,
      width: 150,
    },
    {
      header: "Số điện thoại",
      field: "phone",
      accessor: (plant) => plant.phone,
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

  useEffect(() => {
    console.log(sortField, sortDirection);
  }, [sortField, sortDirection]);

  const renderAction = (plant: GetPlant) => (
    <div>
      <Icons.calendar />
      {/* <button>Sửa</button> */}
      {/* <button>Xóa</button> */}
    </div>
  );

  return (
    <Flex className={style.container}>
      <Flex className={style.plant}>
        <Table
          columns={plantColumns}
          rows={fakePlants}
          rowKey="userCode"
          handleSortClick={handleSortChange}
          selectedColumn={sortField}
          rotation={rotation}
          currentPage={1}
          rowsPerPage={5}
          caption="Bảng quản lý người dùng"
          notifyNoData="Không có người dùng để hiển thị"
          renderAction={(plant: GetPlant) => renderAction(plant)}
        />
      </Flex>
    </Flex>
  );
}

export default PlantList;
