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

        .ant-table-cell-row-hover {
          background-color: ${hoverBackground} !important;
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
        border: 2px solid #3333 !important;
      }

      .ant-checkbox-checked .ant-checkbox-inner {
        background-color: ${primaryColor} !important;
        border-color: ${primaryColor} !important;
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

    customSelect: css`
      .ant-pagination-item-link-icon {
        color: ${primaryColor} !important;
      }

      .ant-select-selector:hover {
        border-color: ${primaryColor} !important;
      }
    `,
    customTab: css`
      .ant-tabs-tab:hover,
      .ant-tabs-tab.ant-tabs-tab-active .ant-tabs-tab-btn {
        color: ${primaryColor};
        outline: none;
      }
      .ant-tabs-ink-bar {
        background-color: ${primaryColor};
      }
    `,
    customInput: css`
      .ant-picker-input > input,
      .ant-input .ant-input-outlined,
      input {
        font-size: 16px !important;
  }
    `,
    customPlaceholder: css`
      .ant-select-selection-placeholder {
        font-size: 16px;
      }
    `
  };
});

export default useStyle;
