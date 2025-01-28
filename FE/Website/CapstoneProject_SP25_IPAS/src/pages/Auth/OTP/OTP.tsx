import { enterOTP } from "@/assets/images/images";
import style from "./OTP.module.scss";
import { Button, Col, Form, Input, Row, Typography } from "antd";
import { useEffect, useRef, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { authService } from "@/services";
import { formatToISO8601, hashOtp } from "@/utils";

const { Title, Text } = Typography;

function OTP() {
  const navigate = useNavigate();
  const location = useLocation();
  const [form] = Form.useForm();
  // const [formRegister, setFormRegister] = useState<any | null>(null);
  const TIME_COUNTER = 120;
  const [timeLeft, setTimeLeft] = useState(TIME_COUNTER);
  const otpRef = useRef<string | null>(location.state?.otp);
  const formRegisterRef = useRef<any | null>(location.state?.values);

  const type = location.state?.type;
  // const formRegister = location.state?.values;

  const instructions =
    type === "sign-up"
      ? "A verification code has been sent to your email. Please enter the code to verify your account."
      : "An OTP has been sent to your email to reset your password. Please enter the code below.";

  useEffect(() => {
    // Kiểm tra `timeLeft` từ localStorage
    const storedTime = localStorage.getItem("otpTimeLeft");
    const now = Date.now();
    const savedTimeLeft = storedTime ? parseInt(storedTime) : 0;

    if (savedTimeLeft > now) {
      // Nếu thời gian chưa hết hạn, tính `timeLeft` còn lại
      setTimeLeft(Math.ceil((savedTimeLeft - now) / 1000));
    } else {
      // Nếu hết hạn, đặt lại về 60 giây
      setTimeLeft(TIME_COUNTER);
    }

    // Xóa timeout khi rời khỏi trang
    return () => {
      localStorage.removeItem("otpTimeLeft");
    };
  }, []);

  useEffect(() => {
    if (timeLeft > 0) {
      // Lưu lại thời gian hết hạn vào localStorage
      const expiryTime = Date.now() + timeLeft * 1000;
      localStorage.setItem("otpTimeLeft", expiryTime.toString());

      const timer = setTimeout(() => {
        setTimeLeft((prev) => prev - 1);
      }, 1000);

      return () => clearTimeout(timer);
    } else {
      // Khi hết thời gian, xóa giá trị lưu trong localStorage
      localStorage.removeItem("otpTimeLeft");
    }
  }, [timeLeft]);

  const handleFinish = async (values: { otp: string }) => {
    const otpUser = await hashOtp(values.otp);
    if (otpUser !== otpRef.current) {
      form.setFields([
        {
          name: "otp",
          errors: ["The OTP you entered is incorrect. Please try again!"],
        },
      ]);
      return;
    }
    if (timeLeft <= 0) {
      form.setFields([
        {
          name: "otp",
          errors: ["The OTP has expired. Please request a new one!"],
        },
      ]);
      return;
    }
    console.log();
    
    if (otpUser === otpRef.current) {
      const registerRequest = {
        email: formRegisterRef.current.email,
        password: formRegisterRef.current.password,
        fullName: formRegisterRef.current.fullName,
        phone: formRegisterRef.current.phoneNumber,
        gender: formRegisterRef.current.gender,
        dob: formatToISO8601(formRegisterRef.current.dateOfBirth),
      };
      var result = await authService.register(registerRequest);
      console.log(result);
      if (result.statusCode === 200) {
      }
    }
  };

  const handleResendOTP = async () => {
    try {
      var result = await authService.sendOTP(formRegisterRef.current.email);
      if (result.statusCode === 200) {
        otpRef.current = result.data.otpHash;
        setTimeLeft(TIME_COUNTER);
      }
    } catch (error) {
      console.error("Error in handleSignUp:", error);
    }
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
          <img src={enterOTP} alt="Enter OTP gif" className={style.otpImg} />
        </Col>

        <Col xs={24} sm={12} md={8}>
          <div className={style.formContainer}>
            <Title level={2} className={style.title}>
              Enter OTP
            </Title>
            <Text className={style.infoText}>{instructions}</Text>
            <div className={style.timerContainer}>
              <Text>Time Remaining:</Text>
              <span className={style.timer}>{formatTime(timeLeft)}</span>
            </div>
            <Form form={form} onFinish={handleFinish} layout="vertical" className={style.form}>
              <Form.Item
                name="otp"
                validateTrigger="onChange"
                rules={[
                  { required: true, message: "Please enter the OTP!" },
                  { len: 6, message: "OTP must be 6 digits!" },
                ]}
                className={style.inputOpt}
              >
                <Input.OTP inputMode="numeric" />
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
                    <Button type="primary" htmlType="submit" block className={style.verifyBtn}>
                      Verify OTP
                    </Button>
                  </Col>
                </Row>
              </Form.Item>
            </Form>
            <div className={style.back}>
              <a
                onClick={() => {
                  navigate("/auth?mode=sign-up", {
                    state: { formRegister: formRegisterRef.current },
                  });
                }}
              >
                Back to sign up
              </a>
            </div>
          </div>
        </Col>
      </Row>
    </div>
  );
}

export default OTP;
