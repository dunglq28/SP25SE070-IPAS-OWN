import { create } from "zustand";
import axios, { AxiosError } from "axios";

interface WeatherState {
  weather: Record<string, any> | null;
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
  place: "Hanoi",
  isLoading: false,
  error: null,
  isSearchVisible: false,

  setPlace: (newPlace) => set({ place: newPlace }),

  fetchWeather: async () => {
    const { place, isLoading } = get();
    if (isLoading) return;

    const API_KEY = "c01125b343b6934e4c8a22f0edc300b7";
    const BASE_URL = `https://api.openweathermap.org/data/2.5/weather`;

    try {
      set({ isLoading: true, error: null });
      const response = await axios.get(BASE_URL, {
        params: {
          q: place,
          appid: API_KEY,
          units: "metric",
        },
      });
      set({ weather: response.data, isLoading: false });
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
