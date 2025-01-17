import { createStyles } from "antd-style";

const useStyle = createStyles(({ css }) => {
  const primaryColor = "#326E2F";
  const hoverBackground = "#f0fff0";

  return {
    customTable: css`
      .ant-table {
        .ant-table-container {
          .ant-table-body,
          .ant-table-content {
            scrollbar-width: thin;
            scrollbar-color: #eaeaea transparent;
            scrollbar-gutter: stable;
          }
        }

        .ant-table-row {
          transition: background-color 0.3s ease;

          &:hover {
            background-color: ${hoverBackground} !important;
          }
        }

        .ant-table-cell-fix-left,
        .ant-table-cell-fix-right {
          &.selectedRowFixed {
            background-color: ${hoverBackground} !important;
            transition: background-color 0.3s ease;
          }

          .ant-table-row:hover & {
            background-color: ${hoverBackground} !important;
          }
        }

        .ant-table-cell-row-hover {
          background-color: transparent !important;
        }

        .ant-table-footer {
          text-align: center;
          padding: 10px 16px;
          background-color: transparent;
        }
      }
    `,

    customCheckbox: css`
      .ant-checkbox-inner {
        border: 2px solid #000;
      }

      .ant-checkbox-checked .ant-checkbox-inner {
        background-color: ${primaryColor} !important;
        border-color: ${primaryColor};
      }

      .ant-checkbox-checked:hover {
        opacity: 0.8;
      }

      .ant-checkbox:hover .ant-checkbox-inner {
        border-color: ${primaryColor} !important;
      }

      .ant-checkbox-inner:after {
        background-color: ${primaryColor};
        border-color: white;
      }
    `,
  };
});

export default useStyle;
