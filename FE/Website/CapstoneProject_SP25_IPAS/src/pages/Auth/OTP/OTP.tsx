import { enterOTP } from "@/assets/images/images";
import style from "./OTP.module.scss";
import { Button, Col, Form, Row, Typography } from "antd";
import { InputOTP } from "antd-input-otp";
import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";

const { Title, Text } = Typography;

function OTP() {
  const [form] = Form.useForm();
  const [otp, setOtp] = useState("");
  const location = useLocation();
  const [timeLeft, setTimeLeft] = useState(60);

  // const params = new URLSearchParams(location.search);
  // const type = params.get("type");
  const type = location.state?.type;

  const instructions =
    type === "sign-up"
      ? "A verification code has been sent to your email. Please enter the code to verify your account."
      : "An OTP has been sent to your email to reset your password. Please enter the code below.";

  useEffect(() => {
    if (timeLeft > 0) {
      const timer = setTimeout(() => {
        setTimeLeft((prev) => prev - 1);
      }, 1000);
      return () => clearTimeout(timer);
    }
  }, [timeLeft]);

  const handleFinish = (values: { otp: string }) => {
    console.log("OTP Submitted:", values.otp);
    if (type === "sign-up") {
      // Logic verify account
    } else if (type === "reset") {
      // Logic reset password
    }
  };

  const handleResendOTP = () => {
    console.log("Resend OTP clicked");
    setTimeLeft(60);
  };

  const formatTime = (seconds: number) => {
    const mins = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${mins < 10 ? `0${mins}` : mins}:${secs < 10 ? `0${secs}` : secs}`;
  };

  return (
    <div className={style.container}>
      <Row justify="center" align="middle" gutter={32}>
        <Col xs={24} sm={12} md={12}>
          <img
            src={enterOTP}
            alt="Enter OTP gif"
            className={style.otpImg}
          />
        </Col>

        <Col xs={24} sm={12} md={8}>
          <div className={style.formContainer}>
            <Title level={2} className={style.title}>
              Enter OTP
            </Title>
            <Text className={style.infoText}>
              {instructions}
            </Text>
            <div className={style.timerContainer}>
              <Text>Time Remaining:</Text>
              <span className={style.timer}>
                {formatTime(timeLeft)}
              </span>
            </div>
            <Form
              onFinish={handleFinish}
              form={form}
              layout="vertical"
              className={style.form}
            >
              <Form.Item
                name="otp"
                rules={[
                  { required: true, message: "Please enter the OTP!" },
                  { len: 6, message: "OTP must be 6 digits!" },
                ]}
              >
                <InputOTP
                  autoSubmit={form}
                  inputType="numeric"
                />
              </Form.Item>

              <Form.Item>
                <Row gutter={16}>
                  <Col span={12}>
                    <Button
                      type="default"
                      onClick={handleResendOTP}
                      block
                      disabled={timeLeft > 0}
                      className={style.resendButton}
                    >
                      Resend OTP
                    </Button>
                  </Col>
                  <Col span={12}>
                    <Button
                      type="primary"
                      htmlType="submit"
                      block
                      className={style.verifyBtn}
                    >
                      Verify OTP
                    </Button>
                  </Col>
                </Row>
              </Form.Item>
            </Form>
            <a className={style.back} href="/auth?mode=sign-in">Back to sign in</a>
          </div>
        </Col>
      </Row>
    </div>
  );
}

export default OTP;
