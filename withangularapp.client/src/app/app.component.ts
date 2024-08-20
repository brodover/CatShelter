import { Component, OnInit } from '@angular/core';

import { LocalStorageService } from './local-storage.service';

import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MessagingComponent } from './messaging/messaging.component';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';

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

  user!: SocialUser;
  loggedIn!: boolean;

  constructor(
    private localStorageService: LocalStorageService,
    private authService: SocialAuthService
  ) { }

  ngOnInit() {
    this.getUsername();
    this.authService.authState.subscribe((user) => {
      console.log(user);
      this.user = user;
      this.loggedIn = (user != null);
    });
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
