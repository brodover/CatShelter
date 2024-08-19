import { Component, OnInit } from '@angular/core';

import { LocalStorageService } from './local-storage.service';

import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MessagingComponent } from './messaging/messaging.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [MessagingComponent, WeatherForecastComponent, FooterComponent, LoginComponent, DashboardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'Cat Shelter';
  username: string = '';

  constructor(
    private localStorageService: LocalStorageService
  ) { }

  ngOnInit() {
    this.getUsername();
  }

  setUsername(user: string) {
    this.username = user;
    this.localStorageService.setItem('Username', user);
  }

  getUsername() {
    var user = this.localStorageService.getItem('Username');
    if (user == null)
      user = new Date().getTime().toString();

    this.setUsername(user);
  }

}
