import { TableColumn } from "@/types";
import { Checkbox, Flex, notification, Table } from "antd";
import style from "./Table.module.scss";
import Loading from "@/components/Loading";
import { useEffect, useState } from "react";
import { Icons } from "@/assets";
import { useStyle } from "@/hooks";
import { ActionBar, Tooltip } from "@/components";

interface TableProps<T> {
  columns: TableColumn<T>[];
  rows: T[];
  rowKey: Extract<keyof T, string>;
  handleSortClick: (field: string) => void;
  selectedColumn: string;
  rotation: number;
  //   isLoading: boolean;
  //   isInitialLoad: boolean;
  currentPage: number;
  rowsPerPage: number;
  caption: string;
  notifyNoData: string;
  renderAction?: (item: T) => React.ReactNode;
  isViewCheckbox?: boolean;
}
const TableComponent = <T extends {}>({
  columns,
  rows,
  rowKey,
  handleSortClick,
  selectedColumn,
  rotation,
  //   isInitialLoad,
  //   isLoading,
  currentPage,
  rowsPerPage,
  caption,
  notifyNoData,
  renderAction,
  isViewCheckbox = false,
}: TableProps<T>) => {
  const { styles } = useStyle();

  const [selection, setSelection] = useState<string[]>([]);

  const hasSelection = selection.length > 0;
  const allSelected = hasSelection && selection.length === rows.length;

  const toggleAllSelection = (isChecked: boolean) => {
    setSelection(isChecked ? rows.map((row) => row[rowKey] as string) : []);
  };

  const toggleRowSelection = (rowId: string) => {
    setSelection((prev) =>
      prev.includes(rowId) ? prev.filter((id) => id !== rowId) : [...prev, rowId],
    );
  };

  useEffect(() => {
    setSelection([]);
  }, [currentPage, rowsPerPage]);

  const deleteSelectedItems = () => {
    console.log(selection);
    notification.success({
      message: "Deleted Successfully",
      description: `${selection.length} items have been deleted.`,
      showProgress: true,
    });
    setSelection([]);
  };

  const antColumns = [
    !isViewCheckbox && {
      title: (
        <Checkbox
          className={styles.customCheckbox}
          checked={allSelected}
          indeterminate={hasSelection && !allSelected}
          onChange={(e) => toggleAllSelection(e.target.checked)}
        />
      ),
      dataIndex: "id",
      key: "id",
      width: 10,
      render: (text: string, record: T) => (
        <Flex>
          <Checkbox
            className={styles.customCheckbox}
            checked={selection.includes(record[rowKey] as string)}
            onChange={() => toggleRowSelection(record[rowKey] as string)}
          />
          <span style={{ marginLeft: "12px" }}>{text}</span>
        </Flex>
      ),
      fixed: "left",
      onCell: (record: T) => ({
        className: selection.includes(record[rowKey] as string) ? style.selectedRow : "",
      }),
    },
    ...columns.map((col) => {
      const isSortable = col.isSort === undefined || col.isSort;
      return {
        title: (
          <div
            className={`${style.headerTbl} ${isSortable ? style.pointer : ""}`}
            onClick={isSortable ? () => handleSortClick(col.field.toString()) : undefined}
          >
            <Tooltip title="Click to sort">
              <Flex className={style.headerCol}>
                <span className={style.headerTitle}>{col.header}</span>
                {isSortable && (
                  <Flex
                    className={style.iconSort}
                    style={{
                      opacity: selectedColumn === col.field ? 1 : undefined,
                      transform: `rotate(${selectedColumn === col.field ? rotation : 360}deg)`,
                      transition: "transform 0.3s ease-in-out",
                    }}
                  >
                    <Icons.sort />
                  </Flex>
                )}
              </Flex>
            </Tooltip>
          </div>
        ),
        dataIndex: col.field,
        key: col.field,
        align: "center",
        width: col.width,
        render: (text: any, record: T) => col.accessor(record) ?? "N/A",
      };
    }),
    renderAction && {
      key: "actions",
      align: "center",
      render: (text: any, record: T) => renderAction(record),
      fixed: "right",
      onCell: (record: T) => ({
        className: selection.includes(record[rowKey] as string) ? style.selectedRow : "",
      }),
      width: 10,
    },
  ].filter(Boolean);

  const dataSource = rows.map((row, index) => ({
    ...row,
    key: row[rowKey] as string,
    id: (currentPage - 1) * rowsPerPage + index + 1,
  }));

  return (
    <>
      <Table
        className={`${style.tbl} ${styles.customTable}`}
        // dataSource={isInitialLoad && isLoading ? [] : dataSource}
        dataSource={dataSource}
        columns={antColumns as any}
        footer={() => <div className={styles.customTable}>{caption}</div>}
        pagination={false}
        // locale={{
        //   emptyText: isInitialLoad && isLoading ? <Loading /> : notifyNoData,
        // }}
        rowClassName={(record) =>
          selection.includes(record[rowKey] as string) ? style.selectedRow : ""
        }
        locale={{
          emptyText: notifyNoData,
        }}
        scroll={{ x: "max-content", y: 70 * 6 }}
      />
      <ActionBar selectedCount={selection.length} deleteSelectedItems={deleteSelectedItems} />
    </>
  );
};
export default TableComponent;
