import { useEffect, useState } from 'react';
import {NavigateFunction, useNavigate } from 'react-router-dom';

interface Forecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

export default function Weather() {
  const [forecasts, setForecasts] = useState<Forecast[]>();
  const navigate = useNavigate();

  useEffect(() => {
    populateWeatherData(navigate);
  }, []);

  const contents = forecasts === undefined
    ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    : <table className="table table-striped" aria-labelledby="tabelLabel">
      <thead>
      <tr>
        <th>Date</th>
        <th>Temp. (C)</th>
        <th>Temp. (F)</th>
        <th>Summary</th>
      </tr>
      </thead>
      <tbody>
      {forecasts.map(forecast =>
        <tr key={forecast.date}>
          <td>{forecast.date}</td>
          <td>{forecast.temperatureC}</td>
          <td>{forecast.temperatureF}</td>
          <td>{forecast.summary}</td>
        </tr>
      )}
      </tbody>
    </table>;

  return (
    <div>
      <h1 id="tabelLabel">Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {contents}
    </div>
  );

  async function populateWeatherData(navigate: NavigateFunction) {
    const response = await fetch('api/v1/weatherforecast');
    
    if (response.status === 401) {
      navigate("/login");
    }
    
    const data = await response.json();
    setForecasts(data);
  }
}