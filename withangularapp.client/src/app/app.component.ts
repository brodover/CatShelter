import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';

import { Cat } from '../data/model';
import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NgIf, NgFor, WeatherForecastComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public myCats: Cat[] =[];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getMyCats();
  }

  getMyCats() {
    this.http.get<Cat[]>('/api/Cats/Get').subscribe({
      next: (result) => { this.myCats = result },  // nextHandler
      error: (error) => { console.error(error); },  // errorHandler 
    });
  }

  title = 'withangularapp.client';
}
