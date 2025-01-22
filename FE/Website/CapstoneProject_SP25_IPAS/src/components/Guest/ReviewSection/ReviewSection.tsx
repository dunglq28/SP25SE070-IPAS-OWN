import React from "react";
import reviews from "@/constants/review";
import { ReviewCard } from "@/components";
import style from "./ReviewSection.module.scss";
const ReviewSection: React.FC = () => {
    return (
        <section className={style.reviewSection}>
            <h2>What Our Customers Say</h2>
            <div className={style.reviewsContainer}>
                {reviews.map((review) => (
                    <ReviewCard
                        key={review.id}
                        content={review.content}
                        username={review.username}
                        role={review.role}
                        rating={review.rating}
                        avatarUrl={review.avatarUrl}
                    />
                ))}
            </div>
        </section>
    );
};

export default ReviewSection;
