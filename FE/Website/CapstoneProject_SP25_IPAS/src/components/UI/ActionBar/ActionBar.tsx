import { Button, Divider, Modal, notification } from "antd";
import { useEffect, useState } from "react";
import style from "./ActionBar.module.scss";
import { Icons } from "@/assets";

interface ActionBarProps {
  selectedCount: number;
  deleteSelectedItems: () => void;
}

export const ActionBar: React.FC<ActionBarProps> = ({ selectedCount, deleteSelectedItems }) => {
  const [isVisible, setIsVisible] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);

  useEffect(() => {
    setIsVisible(selectedCount > 0);
  }, [selectedCount]);

  if (!isVisible) return null;

  const handleDeleteSelectedItems = () => {
    deleteSelectedItems();
    setIsModalVisible(false);
    notification.success({
      message: "Xoá thành công",
      description: `${selectedCount} mục đã được xoá.`,
    });
  };

  const showModal = () => {
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  return (
    <>
      <div className={style.action_bar_container}>
        <div className={`${style.action_bar_content} ${!isVisible ? style.hide : ""}`}>
          <Button className={style.action_bar_btn_selected} disabled>
            {selectedCount} mục được chọn
          </Button>

          <div className={style.divider_container}>
            <Divider className={style.divider} orientation="center" />
          </div>

          <Button
            className={style.action_bar_btn_delete}
            icon={<Icons.delete />}
            onClick={showModal}
          >
            Xoá mục
          </Button>
        </div>
      </div>

      <Modal
        title="Xoá mục đã chọn"
        visible={isModalVisible}
        onCancel={handleCancel}
        onOk={handleDeleteSelectedItems}
        okText="Xoá"
        cancelText="Huỷ"
      >
        <p>
          Bạn có chắc chắn muốn xóa {selectedCount} mục đã chọn? Hành động này không thể hoàn tác.
        </p>
      </Modal>
    </>
  );
};
