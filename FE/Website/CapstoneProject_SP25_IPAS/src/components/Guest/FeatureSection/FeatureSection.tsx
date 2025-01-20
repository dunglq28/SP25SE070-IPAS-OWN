import FeatureCard from "@/components/UI/FeatureCard/FeatureCard";
import style from "./FeatureSection.module.scss";
import Feature from "@/constants/feature";
import image from "@/assets/images/pomeloRed.png";
import { useEffect } from "react";
import AOS from "aos";

const FeatureSection: React.FC = () => {
  useEffect(() => {
    AOS.init({
        duration: 1000,
        easing: 'ease-in-out',
        once: true,
    });

    const handleScroll = () => {
        AOS.refreshHard();
    };

    window.addEventListener("scroll", handleScroll);

    return () => {
        window.removeEventListener("scroll", handleScroll);
    };
}, []);
    return (
        <section className={style.featureSection}>
          <div className={style.featureCards}>
            {Feature.map((feature) => (
              <FeatureCard
              dataAos="zoom-in-up"
                key={feature.id}
                icon={feature.icon}
                title={feature.title}
                description={feature.description}
              />
            ))}
          </div>
          <div className={style.whoWeAre}>
                <div className={style.imageContainer} data-aos="fade-right">
                    <img src={image} alt="Who We Are" />
                </div>
                <div className={style.featureText} data-aos="fade-left">
                    <p className={style.text1}>Who We Are</p>
                    <h3>Currently we are managing and optimizing pomelo farms.</h3>
                    <p>We help pomelo farmers improve productivity and efficiency through AI-driven solutions for crop monitoring, task management, and farm optimization. By integrating real-time data, we assist farmers in making informed decisions about irrigation, fertilization, pest control, and yield predictions.</p>
                </div>
            </div>
        </section>
      );
};

export default FeatureSection;