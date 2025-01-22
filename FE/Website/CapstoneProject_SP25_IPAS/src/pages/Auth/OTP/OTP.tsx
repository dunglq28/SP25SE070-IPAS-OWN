import { enterOTP } from "@/assets/images/images";
import style from "./OTP.module.scss";
import { Button, Col, Form, Row, Typography } from "antd";
import { InputOTP } from "antd-input-otp";
import { useEffect, useState } from "react";

const { Title, Text } = Typography;

function OTP() {
  const [form] = Form.useForm();
  const [otp, setOtp] = useState("");
  const [timeLeft, setTimeLeft] = useState(60); // Countdown in seconds

  // Countdown logic
  useEffect(() => {
    if (timeLeft > 0) {
      const timer = setTimeout(() => {
        setTimeLeft((prev) => prev - 1);
      }, 1000);
      return () => clearTimeout(timer); // Clear timeout when unmounted or timeLeft changes
    }
  }, [timeLeft]);

  const handleFinish = (values: { otp: string }) => {
    console.log("OTP Submitted:", values.otp);
  };

  const handleResendOTP = () => {
    console.log("Resend OTP clicked");
    setTimeLeft(60); // Reset countdown timer
    // Add your resend OTP logic here
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
              An OTP code has been sent to your email. Please check your inbox.
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
            <a className={style.back} href="/sign-in">Back to sign in</a>
          </div>
        </Col>
      </Row>
    </div>
  );
}

export default OTP;
