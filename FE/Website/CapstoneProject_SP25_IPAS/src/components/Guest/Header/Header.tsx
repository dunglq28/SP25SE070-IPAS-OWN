import { logo } from "@/assets/images/images";
import style from "./Header.module.scss";
import NavItem from "@/constants/navItem";
import { ButtonAuth } from "@/components/UI/ButtonAuth/ButtonAuth";
import { useEffect, useState } from "react";

const Header: React.FC = () => {
  const [scrolling, setScrolling] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
        if (window.scrollY > 0) {
            setScrolling(true);
            console.log("scrolling");
            
        } else {
            setScrolling(false);
        }
    };

    window.addEventListener('scroll', handleScroll);

    return () => {
        window.removeEventListener('scroll', handleScroll);
    };
}, []);
  return (
    <header className={`${style.header} ${scrolling ? style.scrolled : ''}`}>
      <a href="/">
      <img
        className={style.img}
        src={logo} alt="IPAS Logo" />
      </a>

      <nav className={style.nav}>
        {NavItem.map((item, index) => (
          <a>{item.label}</a>
        ))}
      </nav>

      <div className={style.authButtons}>
        <ButtonAuth
          label="Sign In"
          href="/auth?mode=sign-in"
          className="button_auth_signin"
        />
        <ButtonAuth
          label="Sign Up"
          href="/auth?mode=sign-up"
          className="button_auth_signup"
        />
      </div>
    </header>
  );
};

export default Header;
