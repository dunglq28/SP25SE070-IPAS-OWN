import React from "react";
import style from "./ReviewCard.module.scss";
import AOS from "aos";

interface ReviewCardProps {
  content: string;
  username: string;
  role: string;
  rating: number;
  avatarUrl?: string;
}

const ReviewCard: React.FC<ReviewCardProps> = ({ content, username, role, rating, avatarUrl }) => {
  return (
    <div
      className={style.reviewCard}
      data-aos="fade-up"
    >
      <div className={style.rating}>
        {Array.from({ length: rating }).map((_, index) => (
          <svg
            key={index}
            width="20"
            height="20"
            viewBox="0 0 24 24"
            fill="gold"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path d="M12 17.27L18.18 21L16.54 13.97L22 9.24L14.81 8.63L12 2L9.19 8.63L2 9.24L7.46 13.97L5.82 21L12 17.27Z" />
          </svg>
        ))}
      </div>
      <p className={style.content}>{`"${content}"`}</p>
      <div
        className={style.userInfo}
        data-aos="fade-up"
        data-aos-delay="200"
      >
        <img
          src={avatarUrl || "/default-avatar.png"}
          alt={`${username}'s avatar`}
          className={style.avatar}
        />
        <div>
          <h4 className={style.username}>{username}</h4>
          <p className={style.role}>{role}</p>
        </div>
      </div>
    </div>
  );
};

export default ReviewCard;
