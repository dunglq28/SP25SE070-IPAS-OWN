import { Button, Divider, Flex, Modal, Popover } from "antd";
import { useEffect, useState } from "react";
import style from "./ActionBar.module.scss";
import { Icons } from "@/assets";

interface ActionBarProps {
  selectedCount: number;
  deleteSelectedItems: () => void;
}

const ActionBar: React.FC<ActionBarProps> = ({ selectedCount, deleteSelectedItems }) => {
  const [isVisible, setIsVisible] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isActionPopupVisible, setIsActionPopupVisible] = useState(false);

  useEffect(() => {
    setIsVisible(selectedCount > 0);
  }, [selectedCount]);

  if (!isVisible) return null;

  const handleDeleteSelectedItems = () => {
    deleteSelectedItems();
    setIsModalVisible(false);
  };

  const showModal = () => {
    setIsModalVisible(true);
  };

  const showActionPopup = () => {
    setIsActionPopupVisible(true); // Hiển thị popup khi bấm "Actions"
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const actionContent = (
    <div className={style.popover_content}>
      <Button className={style.action_popup_btn} onClick={showModal}>
        Delete Items
      </Button>
      {/* Thêm các hành động khác nếu cần */}
    </div>
  );

  return (
    <>
      <Flex className={style.action_bar_container}>
        <Flex className={`${style.action_bar_content} ${!isVisible ? style.hide : ""}`}>
          <Flex className={style.action_bar_selected}>
            <Flex className={style.number_selected_round}>
              <span className={style.number}>{selectedCount} </span>
            </Flex>
            <Flex className={style.text_selected}>items selected</Flex>
          </Flex>

          <Flex className={style.divider_container}>
            <Divider className={style.divider} type="vertical" />
          </Flex>

          <Button
            className={style.action_bar_btn_delete}
            icon={<Icons.delete />}
            onClick={showModal}
          >
            Delete items
          </Button>

          {/* <Popover content={actionContent} trigger="click" placement="bottom">
            <Button className={style.action_bar_btn_actions}>
              Actions <Icons.arrowDown />
            </Button>
          </Popover> */}
        </Flex>
      </Flex>

      <Modal
        title="Delete Selected Items"
        visible={isModalVisible}
        onCancel={handleCancel}
        onOk={handleDeleteSelectedItems}
        okText="Delete"
        cancelText="Cancel"
      >
        <p>
          Are you sure you want to delete the {selectedCount} selected items? This action cannot be
          undone.
        </p>
      </Modal>
    </>
  );
};

export default ActionBar;
