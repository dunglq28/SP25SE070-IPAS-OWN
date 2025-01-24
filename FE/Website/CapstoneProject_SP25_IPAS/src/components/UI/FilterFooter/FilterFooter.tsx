import { Button, Flex } from "antd";
import style from "./FilterFooter.module.scss";

type FooterProps = {
  isFilterEmpty: boolean;
  isFilterChanged: boolean;
  onClear: () => void;
  handleApply: () => void;
};

const FilterFooter = ({ isFilterEmpty, isFilterChanged, onClear, handleApply }: FooterProps) => {
  return (
    <Flex className={style.contentFooter}>
      <Button disabled={isFilterEmpty} onClick={onClear}>
        Clear
      </Button>
      <Button onClick={handleApply} disabled={!isFilterChanged}>
        Apply
      </Button>
    </Flex>
  );
};

export default FilterFooter;
