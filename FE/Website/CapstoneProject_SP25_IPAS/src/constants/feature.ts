
import React from "react";
import { MdOutlineSmartToy } from "react-icons/md";
import { PiFarm } from "react-icons/pi";
import { GiFarmTractor } from "react-icons/gi";
import { GiFarmer } from "react-icons/gi";
import { CiMap } from "react-icons/ci";

interface Feature {
    id: number;
    title: string;
    description: string;
    icon: React.ReactElement;
}

const Feature: Feature[] = [
    {
        id: 1,
        title: "Farm Plot Design",
        description: "Design your farm plots in 2D based on real-world maps.",
        icon: React.createElement(CiMap, { size: 20, color: "green" })
    },
    {
        id: 2,
        title: "Professional Farmers",
        description: "Track crops, manage plots, and monitor real-time soil and weather conditions.",
        icon: React.createElement(PiFarm, { size: 40, color: "green" })
    },
    {
        id: 3,
        title: "AI-Driven Consulting",
        description: "Get image-based pest diagnosis and customized care recommendations.",
        icon: React.createElement(MdOutlineSmartToy, { size: 40, color: "green" })
    },
    {
        id: 4,
        title: "Workforce Management",
        description: "Assign tasks, monitor progress, and boost productivity with ease.",
        icon: React.createElement(GiFarmTractor, { size: 40, color: "green" })
    },
];

export default Feature;
