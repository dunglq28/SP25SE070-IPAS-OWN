import React from "react";
import { Input, Button, DatePicker, Select, Form, Row, Col, Divider } from "antd";
import GoogleButton from "react-google-button";
import style from "./SignUp.module.scss";
import { signInWithPopup } from "firebase/auth";
import { auth, provider } from "@/firebase/config";

interface Props {
    toggleForm: () => void;
    isSignUp: boolean;
}

const SignUp: React.FC<Props> = ({ toggleForm, isSignUp }) => {
    console.log("SignUp", isSignUp);

    const signUpWithGoogle = async () => {
        signInWithPopup(auth, provider).then((data) => {
            console.log(data);
        })
    }

    return (
        <div className={`${style["form-container"]} ${style["sign-up"]} ${!isSignUp ? style.hidden : ""}`} style={{ padding: "50px 100px", textAlign: "center" }}>
            <h1 style={{ fontSize: "30px", marginBottom: "30px" }}>Create Your Account</h1>

            <Form
                name="sign_up"
                initialValues={{ remember: true }}
                layout="vertical"
                style={{ maxWidth: "500px", margin: "0 auto" }}
            >
                <Form.Item
                    name="fullName"
                    rules={[{ required: true, message: "Please input your full name!" }]}
                >
                    <Input placeholder="Full Name" />
                </Form.Item>

                <Row gutter={16}>
                    <Col span={12}>
                        <Form.Item
                            name="email"
                            rules={[{ required: true, message: "Please input your email!" }, { type: "email", message: "Please enter a valid email!" }]}
                        >
                            <Input placeholder="Email" />
                        </Form.Item>
                    </Col>
                    <Col span={12}>
                        <Form.Item
                            name="phoneNumber"
                            rules={[{ required: true, message: "Please input your phone number!" }]}
                        >
                            <Input placeholder="Phone Number" />
                        </Form.Item>
                    </Col>
                </Row>

                <Row gutter={16}>
                    <Col span={12}>
                        <Form.Item
                            name="dateOfBirth"
                            rules={[{ required: true, message: "Please input your date of birth!" }]}
                        >
                            <DatePicker placeholder="Date of Birth" style={{ width: "100%" }} />
                        </Form.Item>
                    </Col>

                    <Col span={12}>
                        <Form.Item
                            name="gender"
                            rules={[{ required: true, message: "Please select your gender!" }]}
                        >
                            <Select placeholder="Gender">
                                <Select.Option value="male">Male</Select.Option>
                                <Select.Option value="female">Female</Select.Option>
                                <Select.Option value="other">Other</Select.Option>
                            </Select>
                        </Form.Item>
                    </Col>
                </Row>

                <Form.Item
                    name="password"
                    rules={[{ required: true, message: "Please input your password!" }]}
                >
                    <Input.Password placeholder="Password" />
                </Form.Item>

                <Form.Item
                    name="confirmPassword"
                    rules={[
                        { required: true, message: "Please confirm your password!" },
                        ({ getFieldValue }) => ({
                            validator(_, value) {
                                if (!value || getFieldValue('password') === value) {
                                    return Promise.resolve();
                                }
                                return Promise.reject(new Error('The two passwords do not match!'));
                            },
                        }),
                    ]}
                >
                    <Input.Password placeholder="Confirm Password" />
                </Form.Item>

                <Form.Item>
                    <Button type="primary" htmlType="submit" block style={{ backgroundColor: "#326E2F", marginTop: "0px" }}>
                        Sign Up
                    </Button>
                </Form.Item>

                <Divider>OR</Divider>

                <GoogleButton
                    style={{ width: "auto" }}
                    label="Sign up with Google"
                    onClick={signUpWithGoogle}
                />
            </Form>
        </div>
    );
};

export default SignUp;
