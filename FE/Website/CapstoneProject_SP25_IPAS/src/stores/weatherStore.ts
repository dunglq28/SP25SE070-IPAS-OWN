import { create } from "zustand";
import axios, { AxiosError } from "axios";

interface WeatherState {
  weather: Record<string, any> | null;
  forecast: Record<string, any>[];
  place: string;
  isLoading: boolean;
  error: string | null;
  isSearchVisible: boolean;
  setPlace: (place: string) => void;
  fetchWeather: () => Promise<void>;
  toggleSearch: () => void;
}

const useWeatherStore = create<WeatherState>((set, get) => ({
  weather: null,
  forecast: [],
  place: "Hanoi",
  isLoading: false,
  error: null,
  isSearchVisible: false,

  setPlace: (newPlace) => set({ place: newPlace }),

  fetchWeather: async () => {
    const { place, isLoading } = get();
    if (isLoading) return;

    const API_KEY = "c01125b343b6934e4c8a22f0edc300b7";
    const CURRENT_WEATHER_URL = `https://api.openweathermap.org/data/2.5/weather`;
    const FORECAST_URL = `https://api.openweathermap.org/data/2.5/forecast`;

    try {
      set({ isLoading: true, error: null });
      // const response = await axios.get(CURRENT_WEATHER_URL, {
      //   params: {
      //     q: place,
      //     appid: API_KEY,
      //     units: "metric",
      //   },
      // });
      // set({ weather: response.data, isLoading: false });
      const currentWeatherPromise = axios.get(CURRENT_WEATHER_URL, {
        params: {
          q: place,
          appid: API_KEY,
          units: "metric",
        },
      });

      // Gọi API dự báo thời tiết
      const forecastPromise = axios.get(FORECAST_URL, {
        params: {
          q: place,
          appid: API_KEY,
          units: "metric",
        },
      });

      const [currentWeatherResponse, forecastResponse] = await Promise.all([
        currentWeatherPromise,
        forecastPromise,
      ]);

      set({
        weather: currentWeatherResponse.data,
        forecast: forecastResponse.data.list, // Lưu dữ liệu dự báo thời tiết
        isLoading: false,
      });
    } catch (error: any) { // Lúc này 'error' được cho kiểu 'any'
      console.error("Weather fetch error:", error);
      // Kiểm tra lỗi chi tiết từ Axios
      const errorMessage = error.response ? error.response.data.message : "Could not fetch weather data. Please try again.";
      set({
        error: errorMessage,
        isLoading: false,
      });
    }
  },

  toggleSearch: () => set((state) => ({ isSearchVisible: !state.isSearchVisible })),
}));

export default useWeatherStore;
