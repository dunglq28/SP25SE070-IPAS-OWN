import { useEffect, useState } from "react";
import style from "./PricingSection.module.scss";
import PricingCard from "@/components/UI/PricingCard/PricingCard";
import AOS from "aos";

interface Package {
    id: number;
    name: string;
    price: string;
    description: string;
    duration: string;
    features: string[];
}

const PricingSection: React.FC = () => {
    const [packages, setPackages] = useState<Package[]>([
        {
            id: 1,
            name: "Basic",
            price: "$29",
            duration: "/month",
            description: "Ideal for small farms or beginners. Provides essential tools for crop management and basic AI care advice.",
            features: ["Farm Plot Management", "Crop Monitoring", "Task Management", "Real-time AI Advice", "Multi-platform Access"],
        },
        {
            id: 2,
            name: "Pro",
            price: "$89",
            duration: "/year",
            description: "Ideal for small farms or beginners. Provides essential tools for crop management and basic AI care advice.",
            features: ["Farm Plot Management", "Crop Monitoring", "Task Management", "Real-time AI Advice", "Multi-platform Access"],
        },
        {
            id: 3,
            name: "Enterprise",
            price: "$100",
            duration: "/3 years",
            description: "Ideal for small farms or beginners. Provides essential tools for crop management and basic AI care advice.",
            features: ["Farm Plot Management", "Crop Monitoring", "Task Management", "Real-time AI Advice", "Multi-platform Access"],
        },
    ]);

    useEffect(() => {
        AOS.init({
            duration: 800,
            easing: "ease-in-out",
            once: false,
        });
    }, []);

    return (
        <section className={style.pricingSection}>
            <p className={style.text1} data-aos="fade-up">
                Our Services
            </p>
            <p className={style.text2} data-aos="fade-up" data-aos-delay="200">
                Best Agriculture Services
            </p>
            <div className={style.pricingCards}>
                {packages.map((pkg, index) => (
                    <div
                        key={pkg.id}
                        data-aos="zoom-in"
                        data-aos-delay={`${index * 100}`}
                    >
                        <PricingCard
                            name={pkg.name}
                            price={pkg.price}
                            duration={pkg.duration}
                            features={pkg.features}
                            description={pkg.description}
                        />
                    </div>
                ))}
            </div>
        </section>
    );
};

export default PricingSection;
