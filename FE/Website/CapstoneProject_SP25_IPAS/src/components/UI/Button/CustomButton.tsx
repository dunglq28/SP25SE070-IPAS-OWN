import style from "./CustomButton.module.scss";
import { Button } from "antd";

interface CustomButtonProps {
  label: string;
  icon?: React.ReactNode;
  handleOnClick?: () => void;
}

const CustomButton: React.FC<CustomButtonProps> = ({ label, icon, handleOnClick }) => {
  return (
    <Button className={style.btn} icon={icon} onClick={handleOnClick}>
      {label}
    </Button>
  );
};

export default CustomButton;
