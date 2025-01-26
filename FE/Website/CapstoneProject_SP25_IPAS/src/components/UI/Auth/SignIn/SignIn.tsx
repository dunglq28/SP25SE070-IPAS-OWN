import React from "react";
import { Input, Button, Form, Space, Divider } from "antd";
import { FaGoogle } from "react-icons/fa";
import style from "./SignIn.module.scss";
import GoogleButton from "react-google-button";
import { GoogleLogin, useGoogleLogin } from "@react-oauth/google";
import { useStyle } from "@/hooks";

interface Props {
  toggleForm: () => void;
  isSignUp: boolean;
}

const SignIn: React.FC<Props> = ({ toggleForm, isSignUp }) => {
  const handleGoogleLoginSuccess = (tokenResponse: any) => {
    console.log("Google Sign-In Success:", tokenResponse);
  };

  const handleGoogleLoginFailure = () => {
    console.error("Google Sign-In Failed");
  };

  const { styles } = useStyle();
  console.log('mệt', styles.customInput);


  return (
    <div
      className={`${style["form-container"]} ${style["sign-in"]} ${isSignUp ? style.hidden : ""}`}
    >
      <Form name="sign_in" initialValues={{ remember: true }} layout="vertical">
        <h1 className={style.formTitle}>Sign In</h1>

        <div className={style["inputGroup"]}>
          <Form.Item
            name="email"
            rules={[
              { required: true, message: "Please input your email!" },
              {
                pattern:
                  /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|org|net|edu|gov|int|mil|coop|aero|museum)$/,
                message: "Please enter a valid email!",
              },
            ]}
          // hasFeedback
          // validateStatus="success"
          // help="Should be combination of numbers & alphabets"
          >
            <Input
              placeholder="Email" style={{fontSize: "16px", backgroundColor: "white", borderRadius: "6px", border: "1px solid #d9d9d9"}} />
          </Form.Item>

          <Form.Item
            name="password"
            rules={[{ required: true, message: "Please input your password!" }]}
          >
            <Input.Password placeholder="Password" className={`${styles.customInput}`} />
          </Form.Item>
        </div>

        <a href="/forgot-password" className={style["forgetpw"]}>
          Forgot Password?
        </a>

        <Form.Item>
          <Button type="primary" htmlType="submit" block style={{ backgroundColor: "#326E2F" }}>
            Sign In
          </Button>
        </Form.Item>

        <Divider>OR</Divider>

        {/* <GoogleButton style={{ width: "auto" }} onClick={handleGoogleButtonClick} /> */}
        <GoogleLogin onSuccess={handleGoogleLoginSuccess} onError={handleGoogleLoginFailure} />
      </Form>
    </div>
  );
};

export default SignIn;
