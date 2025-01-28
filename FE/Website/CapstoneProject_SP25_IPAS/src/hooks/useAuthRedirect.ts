import { LOCAL_STORAGE_KEYS } from "@/constants";
import { PATHS } from "@/routes";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const useAuthRedirect = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const accessToken = localStorage.getItem(LOCAL_STORAGE_KEYS.ACCESS_TOKEN);
    const refreshToken = localStorage.getItem(LOCAL_STORAGE_KEYS.REFRESH_TOKEN);

    if (accessToken && refreshToken) {
      toast.warning("You are already signed in");
    //   navigate(PATHS.);
    }
  }, [navigate]);
};

export default useAuthRedirect;
