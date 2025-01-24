import React, { memo } from "react";
import { PaginationButtonProps } from "@/types";
import { Button } from "antd";

const PaginationButton: React.FC<PaginationButtonProps> = memo(
  ({ onClick, isDisabled, hoverStyles, text, icon }) => {
    return (
      <Button
        onClick={onClick}
        disabled={isDisabled}
        style={{
          ...hoverStyles,
        }}
      >
        {icon}
        {text}
      </Button>
    );
  },
);

export default PaginationButton;
