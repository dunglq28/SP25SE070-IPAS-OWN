import { LOCAL_STORAGE_KEYS } from "@/constants";
import { PATHS } from "@/routes";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const useAuthRedirect = (): boolean => {
  const [isAuthChecked, setIsAuthChecked] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const accessToken = localStorage.getItem(LOCAL_STORAGE_KEYS.ACCESS_TOKEN);
    const refreshToken = localStorage.getItem(LOCAL_STORAGE_KEYS.REFRESH_TOKEN);

    if (!accessToken && !refreshToken) {
      toast.warning("You do not have permission to access this resource");
      navigate(PATHS.AUTH.LANDING);
    } else {
      setIsAuthChecked(true);
    }
  }, [navigate]);

  return isAuthChecked;
};

export default useAuthRedirect;
