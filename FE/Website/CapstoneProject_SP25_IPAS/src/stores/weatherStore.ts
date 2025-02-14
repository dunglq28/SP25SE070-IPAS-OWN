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

    const API_KEY = import.meta.env.VITE_API_KEY;
    const CURRENT_WEATHER_URL = `https://api.openweathermap.org/data/2.5/weather`;
    const FORECAST_URL = `https://api.openweathermap.org/data/2.5/forecast`;

    try {
      set({ isLoading: true, error: null });
      const currentWeatherPromise = axios.get(CURRENT_WEATHER_URL, {
        params: {
          q: place,
          appid: API_KEY,
          units: "metric",
        },
      });

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
        forecast: forecastResponse.data.list,
        isLoading: false,
      });
    } catch (error: any) { 
      console.error("Weather fetch error:", error);
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
