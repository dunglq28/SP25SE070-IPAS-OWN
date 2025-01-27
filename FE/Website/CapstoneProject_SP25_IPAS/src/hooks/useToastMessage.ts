import { useRef, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const useToastMessage = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const flag = useRef(false);

  useEffect(() => {
    const toastMessage = location.state?.toastMessage;
    if (toastMessage && !flag.current) {
      toast.success(toastMessage, { autoClose: 2500 });
      flag.current = true;
      navigate(location.pathname, { replace: true });
    }
  }, [location.state, navigate, location.pathname]);
};

export default useToastMessage;
