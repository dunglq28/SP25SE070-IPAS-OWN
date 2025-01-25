import useWeatherStore from "@/stores/weatherStore";
import { Button, Input, Spin, Alert, Typography } from "antd";
import { SearchOutlined } from "@ant-design/icons";
import { useEffect, useState } from "react";

const { Title, Text } = Typography;

function Weather() {
    const { weather, isLoading, error, place, setPlace, fetchWeather, isSearchVisible, toggleSearch } = useWeatherStore((state) => state);

  const [searchValue, setSearchValue] = useState<string>(""); // State for managing input value

  // Call fetchWeather when component mounts or when place changes
  useEffect(() => {
    fetchWeather();
  }, [place, fetchWeather]);

  const handleSearch = () => {
    if (searchValue) {
      setPlace(searchValue); // Set the place from the search input
    }
  };
  console.log("Weather", weather);
  

  return (
    <div style={{ maxWidth: "600px", margin: "auto", padding: "20px" }}>
      {/* Toggle search visibility */}
      <Button onClick={toggleSearch} style={{ marginBottom: "20px" }}>
        {isSearchVisible ? "Hide Search" : "Show Search"}
      </Button>

      {/* Conditional rendering for search box */}
      {isSearchVisible && (
        <Input
          placeholder="Enter location"
          prefix={<SearchOutlined />}
          value={searchValue} // Bind the input value to state
          onChange={(e) => setSearchValue(e.target.value)} // Update the state when the input changes
          onPressEnter={handleSearch} // Trigger search when Enter is pressed
          style={{ marginBottom: "20px" }}
        />
      )}

      {/* Display weather information */}
      {isLoading ? (
        <Spin size="large" />
      ) : error ? (
        <Alert message={error} type="error" showIcon />
      ) : (
        weather && (
          <>
            <Title level={2}>Weather in {place}</Title>
            <Text>Temperature: {weather.main?.temp}°C</Text>
            <br />
            <Text>Weather: {weather.weather[0]?.description}</Text>
            <br />
            <Text>Humidity: {weather.main?.humidity}%</Text>
          </>
        )
      )}
    </div>
  );
}

export default Weather;