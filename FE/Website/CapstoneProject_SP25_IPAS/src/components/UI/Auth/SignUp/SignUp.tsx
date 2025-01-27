import React, { useEffect } from "react";
import { Input, Button, DatePicker, Select, Form, Row, Col, Divider } from "antd";
import GoogleButton from "react-google-button";
import style from "./SignUp.module.scss";
import { useNavigate } from "react-router-dom";
import { useStyle } from "@/hooks";

interface Props {
    toggleForm: () => void;
    isSignUp: boolean;
}

const SignUp: React.FC<Props> = ({ toggleForm, isSignUp }) => {
    const [form] = Form.useForm();
    const navigate = useNavigate();
    const { styles } = useStyle();

    useEffect(() => {
        if (!isSignUp) {
          form.resetFields();
        }
      }, [isSignUp, form]);

    console.log("SignUp", isSignUp);
    

    const signUpWithGoogle = async () => {
        // signInWithPopup(auth, provider).then((data) => {
        //     console.log(data);
        // })
    }

    const handleSignUp = async (values: any) => {

        try {
            // send otp request

            if (true) {
                console.log("OTP sent successfully!");

                navigate("/sign-up/otp", { state: { type: "sign-up" } });
            } else {
                console.log("OTP failed to send!");

            }
        } catch (error: any) {
            console.error(error);
        }
    };


    return (
        <div className={`${style["form-container"]} ${style["sign-up"]} ${!isSignUp ? style.hidden : ""}`} style={{ padding: "50px 100px", textAlign: "center" }}>
            <h1 style={{ fontSize: "30px", marginBottom: "30px" }}>Create Your Account</h1>

            <Form
                form={form}
                name="sign_up"
                initialValues={{ remember: true }}
                layout="vertical"
                style={{ maxWidth: "500px", margin: "0 auto", fontSize: "16px" }}
                onFinish={handleSignUp}
            >
                <Form.Item
                    name="fullName"
                    rules={[{ required: true, message: "Please input your full name!" }]}
                >
                    <Input
                        placeholder="Full Name"
                        style={{
                            backgroundColor: "white",
                            borderRadius: "6px",
                            border: "1px solid #d9d9d9",
                            fontSize: "16px",
                            margin: "0px"
                        }}
                    />
                </Form.Item>

                <Row gutter={16}>
                    <Col span={12}>
                        <Form.Item
                            name="email"
                            rules={[
                                { required: true, message: "Please input your email!" },
                                {
                                    pattern: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|org|net|edu|gov|int|mil|coop|aero|museum)$/,
                                    message: "Please enter a valid email!"
                                }
                            ]}
                        >
                            <Input
                                placeholder="Email"
                                style={{
                                    backgroundColor: "white",
                                    borderRadius: "6px",
                                    border: "1px solid #d9d9d9",
                                    fontSize: "16px",
                                }} />
                        </Form.Item>
                    </Col>
                    <Col span={12}>
                        <Form.Item
                            name="phoneNumber"
                            rules={[{ required: true, message: "Please input your phone number!" }]}
                        >
                            <Input
                                placeholder="Phone Number"
                                style={{
                                    backgroundColor: "white",
                                    borderRadius: "6px",
                                    border: "1px solid #d9d9d9",
                                    fontSize: "16px",
                                }} />
                        </Form.Item>
                    </Col>
                </Row>

                <Row gutter={16}>
                    <Col span={12}>
                        <Form.Item
                            name="dateOfBirth"
                            rules={[{ required: true, message: "Please input your date of birth!" }]}
                        >
                            <DatePicker placeholder="Date of Birth" style={{ width: "100%", fontSize: "16px" }} className={`${styles.customInput}`} />
                        </Form.Item>
                    </Col>

                    <Col span={12}>
                        <Form.Item
                            name="gender"
                            rules={[{ required: true, message: "Please select your gender!" }]}

                        >
                            <Select placeholder="Gender" style={{ height: "50px", fontSize: "16px" }} className={`${styles.customPlaceholder}`}>
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
                    <Input.Password placeholder="Password" className={`${styles.customInput}`} />
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
                    <Input.Password placeholder="Confirm Password" className={`${styles.customInput}`} />
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
