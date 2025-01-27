import React, { useEffect, useState } from "react";
import { Input, Button, Form, Divider, Flex } from "antd";
import style from "./SignIn.module.scss";
import { GoogleCredentialResponse } from "@react-oauth/google";
import { GoogleLoginButton } from "@/components";
import { useStyle } from "@/hooks";
import { RulesManager } from "@/utils";
import { authService } from "@/services";
import { useNavigate } from "react-router-dom";
import { PATHS } from "@/routes";
import { toast } from "react-toastify";

interface Props {
  toggleForm: () => void;
  isSignUp: boolean;
  handleGoogleLoginSuccess: (response: GoogleCredentialResponse) => Promise<void>;
}

const SignIn: React.FC<Props> = ({ toggleForm, isSignUp, handleGoogleLoginSuccess }) => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const { styles } = useStyle();
  const [form] = Form.useForm();

  useEffect(() => {
    if (!isSignUp) {
      form.resetFields();
    }
  }, [isSignUp, form]);

  const handleSubmit = async (values: any) => {
    try {
      setIsLoading(true);
      const result = await authService.login(values.email, values.password);
      const toastMessage = result.message;
      setTimeout(() => {
        setIsLoading(false);
        if (result.statusCode === 200) {
          navigate(PATHS.FARM_PICKER, { state: { toastMessage } });
        } else {
          toast.error(toastMessage);
        }
      }, 1500);
    } catch (error) {
      setIsLoading(false);
      console.error("Error submitting form:", error);
    }
  };

  return (
    <div
      className={`${style["form-container"]} ${style["sign-in"]} ${isSignUp ? style.hidden : ""}`}
    >
      <Form
        name="sign_in"
        form={form}
        onFinish={handleSubmit}
        initialValues={{ remember: true }}
        layout="vertical"
      >
        <h1 className={style.formTitle}>Sign In</h1>

        <div className={style["inputGroup"]}>
          <Form.Item name="email" rules={RulesManager.getEmailRules()} hasFeedback>
            <Input
              placeholder="Email"
              style={{
                fontSize: "16px",
                backgroundColor: "white",
                borderRadius: "6px",
                border: "1px solid #d9d9d9",
              }}
            />
          </Form.Item>

          <Form.Item name="password" rules={RulesManager.getPasswordRules()} hasFeedback>
            <Input.Password placeholder="Password" className={`${styles.customInput}`} />
          </Form.Item>
        </div>

        <Flex className={style.forgetWrapper}>
          <a href="/forgot-password" className={style["forgetpw"]}>
            Forgot Password?
          </a>
        </Flex>

        <Form.Item>
          <Button
            loading={isLoading}
            type="primary"
            htmlType="submit"
            block
            className={style.btn_signin}
          >
            Sign In
          </Button>
        </Form.Item>

        <Divider>OR</Divider>

        <GoogleLoginButton onSuccess={handleGoogleLoginSuccess} />
      </Form>
    </div>
  );
};

export default SignIn;
