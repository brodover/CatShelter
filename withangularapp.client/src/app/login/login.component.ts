import { Component, OnInit } from '@angular/core';
import { GoogleSigninButtonModule, SocialAuthService, SocialLoginModule, SocialUser } from '@abacritt/angularx-social-login';
import { AsyncPipe, NgIf } from '@angular/common';
import { UserAuthService } from '../user-auth.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [NgIf, AsyncPipe, SocialLoginModule, GoogleSigninButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  playerName$: Observable<string>;
  playerName!: string;
  subscriptions: Subscription[] = []

  constructor(
    private saService: SocialAuthService,
    private uaService: UserAuthService
  ) {
    this.playerName$ = this.uaService.playerName$;
  }

  ngOnInit() {

    this.subscriptions.push(this.saService.authState.subscribe(user => {
      this.uaService.setPlayerName(user.firstName);
      console.log(user);
    }));

    this.subscriptions.push(this.uaService.playerName$.subscribe(val => {
      this.playerName = val;
      console.log(`init: ${val}`);
    }));
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sub => sub.unsubscribe()) 
  }
}
