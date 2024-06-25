import { NgFor, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  Date: string;
  TemperatureC: number;
  TemperatureF: number;
  Summary: string;
}

@Component({
  selector: 'app-weather-forecast',
  standalone: true,
  imports: [ NgIf, NgFor ],
  templateUrl: './weather-forecast.component.html',
  styleUrl: './weather-forecast.component.css'
})
export class WeatherForecastComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/api/weatherforecast').subscribe({
      //complete: () => {  }, // completeHandler
      next: (result) => { this.forecasts = result },  // nextHandler
      error: (error) => { console.error(error); },  // errorHandler 
    });
  }

  title = 'withangularapp.client';
}
