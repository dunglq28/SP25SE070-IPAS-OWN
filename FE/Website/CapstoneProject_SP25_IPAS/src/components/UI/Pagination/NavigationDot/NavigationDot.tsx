import React, { useCallback, useMemo } from "react";
import { Pagination, Row, Col, Select, Typography, Flex } from "antd";
import style from "./NavigationDot.module.scss";
import { NavigationDotProps } from "@/types";
import { Icons } from "@/assets";
import { useStyle } from "@/hooks";

const { Option } = Select;
const { Text } = Typography;

const NavigationDot: React.FC<NavigationDotProps> = ({
  totalPages,
  currentPage = 1,
  rowsPerPage = 5,
  onPageChange,
  rowsPerPageOptions,
  onRowsPerPageChange,
}) => {
  const { styles } = useStyle();
  const handlePageChange = useCallback(
    (newPage: number) => {
      if (newPage >= 1 && newPage <= totalPages && newPage !== currentPage) {
        onPageChange(newPage);
      }
    },
    [currentPage, totalPages, onPageChange],
  );

  const handleRowsPerPageChange = useCallback(
    (value: string) => {
      const newRowsPerPage = parseInt(value, 10);
      onRowsPerPageChange(newRowsPerPage);
    },
    [onRowsPerPageChange],
  );

  const paginationProps = useMemo(
    () => ({
      current: currentPage,
      total: totalPages * rowsPerPage,
      pageSize: rowsPerPage,
      onChange: handlePageChange,
      showSizeChanger: false,
    }),
    [currentPage, totalPages, rowsPerPage, handlePageChange],
  );

  return (
    <Row className={style.navigationContainer}>
      <Col className={style.paginationWrapper}>
        <Pagination
          className={`${style.paginationDotWrapper}  ${styles.customSelect}`}
          {...paginationProps}
          itemRender={(page, type, originalElement) => {
            if (type === "prev") {
              return <Flex className={style.paginationArrow}>{<Icons.arrowBack />}</Flex>;
            }
            if (type === "next") {
              return <Flex className={style.paginationArrow}>{<Icons.arrowForward />}</Flex>;
            }
            if (type === "page") {
              const isActive = page === paginationProps.current;
              return (
                <Flex className={`${style.paginationDot} ${isActive ? style.active : ""}`}>
                  {page}
                </Flex>
              );
            }
            return originalElement;
          }}
        />
      </Col>

      <Col className={style.rowsPerPageWrapper}>
        <Row align="middle" gutter={8}>
          <Col>
            <Text>Rows per page:</Text>
          </Col>
          <Col>
            <Select
              value={rowsPerPage.toString()}
              onChange={handleRowsPerPageChange}
              showSearch
              className={`${style.navDotSelect}  ${styles.customSelect}`}
            >
              {rowsPerPageOptions.map((row) => (
                <Option key={row} value={row.toString()}>
                  <span className={style.navDotSelectText}>{row}</span>
                </Option>
              ))}
            </Select>
          </Col>
        </Row>
      </Col>
    </Row>
  );
};

export default NavigationDot;
