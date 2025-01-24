import { Flex } from "antd";
import style from "./PlantList.module.scss";
import { ActionMenuPlant, NavigationDot, SectionTitle, Table } from "@/components";
import { GetPlant } from "@/payloads";
import { useFetchData } from "@/hooks";
import { useEffect, useState } from "react";
import { getOptions } from "@/utils";
import { userService } from "@/services";
import PlantFilter from "./PlantFilter";
import { plantColumns } from "./PlantColumns";
import { TableTitle } from "./TableTitle";


function PlantList() {
  const [filters, setFilters] = useState({
    createDateFrom: "",
    createDateTo: "",
    growthStages: [] as string[],
    status: [] as string[],
  });

  const {
    data,
    fetchData,
    totalRecords,
    totalPages,
    sortField,
    rotation,
    handleSortChange,
    currentPage,
    rowsPerPage,
    searchValue,
    handlePageChange,
    handleRowsPerPageChange,
    handleSearch,
    isLoading,
    isInitialLoad,
  } = useFetchData<GetPlant>({
    fetchFunction: (page, limit, sortField, sortDirection, searchValue) =>
      userService.getUsers(page, limit, sortField, sortDirection, searchValue, "21", filters),
  });

  useEffect(() => {
    fetchData();
  }, [currentPage, rowsPerPage, sortField, searchValue]);

  const updateFilters = (key: string, value: any) => {
    setFilters((prev) => ({ ...prev, [key]: value }));
  };

  const handleApply = () => {
    fetchData();
  };

  const handleClear = () => {
    setFilters({
      createDateFrom: "",
      createDateTo: "",
      growthStages: [],
      status: [],
    });
  };

  const filterContent = (
    <PlantFilter
      filters={filters}
      updateFilters={updateFilters}
      onClear={handleClear}
      onApply={handleApply}
    />
  );

  const fakeData: GetPlant[] = [
    {
      userCode: "USR001",
      userId: 1,
      fullname: "Nguyễn Văn A",
      userName: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
      phone: "0123456789",
      roleId: 2,
      isActive: true,
      status: 1,
    },
    {
      userCode: "USR002",
      userId: 2,
      fullname: "Trần Thị B",
      userName: "tranthib",
      phone: "0987654321",
      roleId: 3,
      isActive: false,
      status: 0,
    },
    {
      userCode: "USR003",
      userId: 3,
      fullname: "Lê Văn C",
      userName: "levanc",
      phone: "0912345678",
      roleId: 1,
      isActive: true,
      status: 1,
    },
    {
      userCode: "USR004",
      userId: 4,
      fullname: "Phạm Thị D",
      userName: "phamthid",
      phone: "0932123456",
      roleId: 4,
      isActive: false,
      status: 2,
    },
    {
      userCode: "USR005",
      userId: 5,
      fullname: "Hoàng Văn E",
      userName: "hoangvane",
      phone: "0909123456",
      roleId: 2,
      isActive: true,
      status: 1,
    },
  ];

  return (
    <Flex className={style.container}>
      <SectionTitle title="Plant Management" totalRecords={totalRecords} />
      <Flex className={style.table}>
        <Table
          columns={plantColumns}
          rows={data}
          rowKey="userCode"
          title={<TableTitle onSearch={handleSearch} filterContent={filterContent} />}
          handleSortClick={handleSortChange}
          selectedColumn={sortField}
          rotation={rotation}
          currentPage={currentPage}
          rowsPerPage={rowsPerPage}
          isLoading={isLoading}
          isInitialLoad={isInitialLoad}
          caption="Bảng quản lý người dùng"
          notifyNoData="Không có người dùng để hiển thị"
          renderAction={(plant: GetPlant) => <ActionMenuPlant id={plant.userId} />}
        />

        <NavigationDot
          totalPages={totalPages}
          currentPage={currentPage}
          rowsPerPage={rowsPerPage}
          onPageChange={handlePageChange}
          rowsPerPageOptions={getOptions(totalRecords)}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </Flex>
    </Flex>
  );
}

export default PlantList;
