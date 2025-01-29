import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import useAuth from "./useAuth";
import { PATHS } from "@/routes";
import { authService } from "@/services";

const useLogout = () => {
  const navigate = useNavigate();
  const { getAuthData, clearAuthData } = useAuth();

  const handleLogout = async () => {
    const authData = getAuthData();
    if (authData.refreshToken && authData.accessToken) {
      var result = await authService.logout(authData.refreshToken);
      if (result.statusCode === 200) {
        clearAuthData();
        navigate(PATHS.AUTH.LANDING);
      }
    }
  };

  return handleLogout;
};

export default useLogout;
