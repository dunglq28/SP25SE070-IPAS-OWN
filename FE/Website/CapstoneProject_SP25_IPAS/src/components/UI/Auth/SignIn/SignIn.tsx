import React from "react";
import { Input, Button, Form, Space, Divider } from "antd";
import { FaGoogle } from "react-icons/fa";
import style from "./SignIn.module.scss";
import GoogleButton from "react-google-button";
import { signInWithPopup } from "firebase/auth";
import { auth, provider } from "@/firebase/config";

interface Props {
    toggleForm: () => void;
    isSignUp: boolean;
}

const SignIn: React.FC<Props> = ({ toggleForm, isSignUp }) => {
    console.log("SignIn", isSignUp);

    const handleSignInGoogle = async () => {
        try {
            const result = await signInWithPopup(auth, provider);
            const user = result.user;
            console.log("Signed in with Google:", user);
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <div className={`${style["form-container"]} ${style["sign-in"]} ${isSignUp ? style.hidden : ""}`}>
            <Form
                name="sign_in"
                initialValues={{ remember: true }}
                onFinish={handleSignInGoogle}
                layout="vertical"
            >
                <h1 style={{ fontSize: "40px" }}>Sign In</h1>

                <div className={style["inputGroup"]}>
                    <Form.Item
                        name="email"
                        rules={[
                            { required: true, message: "Please input your email!" },
                            { type: "email", message: "Please enter a valid email!" }
                        ]}
                    >
                        <Input placeholder="Email" />
                    </Form.Item>

                    <Form.Item
                        name="password"
                        rules={[
                            { required: true, message: "Please input your password!" }
                        ]}
                    >
                        <Input.Password placeholder="Password" />
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

                <GoogleButton
                    style={{ width: "auto" }}
                    onClick={handleSignInGoogle}
                />
            </Form>
        </div>
    );
};

export default SignIn;
