import { useLocation } from "react-router-dom";
import style from "./PlantDetail.module.scss";
import { Divider, Flex, Image, Tag } from "antd";
import { Icons } from "@/assets";
import { Tooltip } from "@/components";

const InfoField = ({
  icon: Icon,
  label,
  value,
}: {
  icon: React.ElementType;
  label: string;
  value: string;
}) => (
  <Flex className={style.infoField}>
    <Flex className={style.fieldLabelWrapper}>
      <Icon className={style.fieldIcon} />
      <label className={style.fieldLabel}>{label}:</label>
    </Flex>
    <label className={style.fieldValue}>{value}</label>
  </Flex>
);

const SectionHeader = ({
  title,
  code,
  status,
}: {
  title: string;
  code: string;
  status: string;
}) => (
  <Flex className={style.contentSectionHeader}>
    <Flex className={style.contentSectionTitle}>
      <Flex className={style.contentSectionTitleLeft}>
        <label className={style.title}>{title}</label>
        <Tooltip title="Hello">
          <Icons.tag className={style.iconTag} />
        </Tooltip>
        <Tag
          className={`${style.statusTag} ${
            status.toLowerCase() === "normal" ? style.normal : style.issue
          }`}
        >
          {status}
        </Tag>
      </Flex>
      <Flex>
        <Icons.edit
          className={style.iconEdit}
          onClick={() => {
            console.log(1);
          }}
        />
      </Flex>
    </Flex>
    <label className={style.subTitle}>Code: {code}</label>
  </Flex>
);

const DescriptionSection = ({ description, images }: { description: string; images: string[] }) => (
  <Flex className={style.descriptionSection}>
    <Flex className={`${style.infoField} ${style.infoFieldColumn}`}>
      <Flex className={style.fieldLabelWrapper}>
        <Icons.description className={style.fieldIcon} />
        <label className={style.fieldLabel}>Description:</label>
      </Flex>
      <label className={style.fieldValue}>{description}</label>
      <Flex className={style.imageWrapper}>
        {images.map((src, index) => (
          <Image key={index} width={160} src={src} />
        ))}
      </Flex>
    </Flex>
  </Flex>
);

function PlantDetail() {
  const location = useLocation();
  const pathnames = location.pathname.split("/");
  const secondLastPart = pathnames[pathnames.length - 2];

  const infoFieldsLeft = [
    { label: "Growth Status", value: "Full-growth", icon: Icons.growth },
    { label: "Plant Lot", value: "Green Pomelo Lot 1", icon: Icons.box },
    { label: "Create At", value: "Sunday, 1st December 2024, 8:30 A.M", icon: Icons.time },
  ];

  const infoFieldsRight = [
    {
      label: "Plant Location",
      value: "Green Pomelo Plot 2 - Row A - Plant #2",
      icon: Icons.location,
    },
    { label: "Cultivar", value: "Green Skin Pomelo", icon: Icons.plant },
  ];

  const description =
    "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.";

  const images = [
    "https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png",
    "https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png",
  ];

  return (
    <Flex className={style.contentWrapper}>
      <SectionHeader title="Green skin pomelo 1" code="WL00000001" status="Normal" />
      <Divider className={style.divider} />
      <Flex className={style.contentSectionBody}>
        <Flex className={style.col}>
          {infoFieldsLeft.map((field, index) => (
            <InfoField key={index} icon={field.icon} label={field.label} value={field.value} />
          ))}
        </Flex>
        <Flex className={style.col}>
          {infoFieldsRight.map((field, index) => (
            <InfoField key={index} icon={field.icon} label={field.label} value={field.value} />
          ))}
        </Flex>
      </Flex>
      <DescriptionSection description={description} images={images} />
    </Flex>
  );
}

export default PlantDetail;
