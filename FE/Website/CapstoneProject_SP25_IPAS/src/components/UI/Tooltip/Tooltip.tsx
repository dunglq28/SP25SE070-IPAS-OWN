import React from "react";
import { Tooltip } from "antd";
import style from "./Tooltip.module.scss";

interface CustomTooltipProps {
  title: string;
  children: React.ReactNode;
  placement?: "top" | "right" | "bottom" | "left";
  color?: string;
}

const CustomTooltip: React.FC<CustomTooltipProps> = ({
  title,
  children,
  placement = "top",
  color = "#bcd379",
}) => {
  return (
    <Tooltip
      title={<div className={style.tooltipTitle}>{title}</div>}
      placement={placement}
      color={color}
    >
      {children}
    </Tooltip>
  );
};

export default CustomTooltip;
