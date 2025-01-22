import React, { FormEvent, useState } from "react";
import { Form, Input, Button, Row, Col, Typography } from "antd";
import { forgotPassword } from "@/assets/images/images";
import style from "./ForgetPassword.module.scss";
import { useNavigate } from "react-router-dom";

const { Title, Text } = Typography;

function ForgetPassword() {
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const navigate = useNavigate();

  const forgetPassword = async (email: string) => {
    console.log(email);
    
    setTimeout(() => {
        navigate("otp");
    }, 2000);
  };

  return (
    <div className={style.forgetpasswordContainer}>
      <Row justify="center" align="middle" gutter={32}>
        <Col xs={24} sm={12} md={12}>
          <img src={forgotPassword} alt="Forgot password gif" className={style.gifImage} />
        </Col>

        <Col xs={24} sm={12} md={8}>
          <div className={style.formContainer}>
            <Title level={2}>Forgot Your Password?</Title>
            <Form onFinish={forgetPassword}>
              <Form.Item
                name="email"
                rules={[
                    { required: true, message: "Please input your email!" },
                    {
                      pattern: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
                      message: "Please enter a valid email!"
                    }
                  ]}
              >
                <Input
                  type="email"
                  placeholder="Enter your email address"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  style={{ padding: "10px 10px", margin: "10px 0" }}
                />
              </Form.Item>

              <Form.Item>
                <Button className={style.btn} htmlType="submit" block>
                  Next
                </Button>
              </Form.Item>
            </Form>

            <a className={style.back} href="/sign-in">Back to sign in</a>

            {error && <Text type="danger">{error}</Text>}
            {success && <Text type="success">{success}</Text>}
          </div>
        </Col>
      </Row>
    </div>
  );
}

export default ForgetPassword;
