import { Component } from '@angular/core';
import { Observable } from 'rxjs';

import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MessagingComponent } from './messaging/messaging.component';
import { UserAuthService } from './user-auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [MessagingComponent, WeatherForecastComponent, FooterComponent, LoginComponent, DashboardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Cat Shelter';
  playerName$: Observable<string>;

  constructor(
    private uaService: UserAuthService
  ) {
    this.playerName$ = uaService.playerName$;
  }
  
  setPlayerName(name: string) {
    this.uaService.setPlayerName(name);
  }
}
