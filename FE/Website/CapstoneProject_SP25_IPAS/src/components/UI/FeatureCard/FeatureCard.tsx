import style from "./FeatureCard.module.scss";

interface FeatureCardProps {
    icon: React.ReactNode;
    title: string;
    description: string;
    dataAos: string;
}

const FeatureCard: React.FC<FeatureCardProps> = ({icon, title, description, dataAos }) => {
    return (
        <div className={style.featureCard} data-aos={dataAos}>
            <div className={style.iconTitle}>
            <div className={style.icon}>{icon}</div> 
            <h3 className={style.title}>{title}</h3> 
            </div>
            <hr/>
            <p className={style.description}>{description}</p>
        </div>
    );
};

export default FeatureCard; 