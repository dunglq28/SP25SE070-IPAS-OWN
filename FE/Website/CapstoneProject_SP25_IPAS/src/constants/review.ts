
import React from "react";

interface Review {
    id: number;
    content: string;
    username: string;
    role: string;
    rating: number;
    avatarUrl: string;
}

const Review = [
    {
        id: 1,
        content: "“I would recommend practitioners at this center to everyone! They are great to work with and are excellent trainers. Thank you all!”",
        username: "Alice Johnson",
        role: "Owner",
        rating: 5,
        avatarUrl: "https://i.pravatar.cc/150?img=5",
    },
    {
        id: 2,
        content: "“I would recommend practitioners at this center to everyone! They are great to work with and are excellent trainers. Thank you all!”",
        username: "Michael Smith",
        role: "Owner",
        rating: 4,
        avatarUrl: "https://i.pravatar.cc/150?img=15",
    },
    {
        id: 3,
        content: "“I would recommend practitioners at this center to everyone! They are great to work with and are excellent trainers. Thank you all!”",
        username: "Sophia Brown",
        role: "Owner",
        rating: 5,
        avatarUrl: "https://i.pravatar.cc/150?img=10",
    },
];



export default Review;
