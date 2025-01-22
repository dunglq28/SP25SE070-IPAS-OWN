import style from "./SectionTitle.module.scss";
import { Flex, Typography } from "antd";
const Text = Typography;

interface SectionTitleProps {
  title: string;
  totalRecords?: number;
}

const SectionTitle: React.FC<SectionTitleProps> = ({ title, totalRecords }) => {
  return (
    <Flex className={style.titleWrapper}>
      <Text className={style.title}>{title}</Text>
      {totalRecords !== undefined && <Text className={style.titleNumber}>({totalRecords})</Text>}
    </Flex>
  );
};

export default SectionTitle;
