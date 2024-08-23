import { Component, OnInit } from '@angular/core';
import { GoogleSigninButtonModule, SocialAuthService, SocialLoginModule, SocialUser } from '@abacritt/angularx-social-login';
import { AsyncPipe, NgIf } from '@angular/common';
import { UserAuthService } from '../user-auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [NgIf, AsyncPipe, SocialLoginModule, GoogleSigninButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  playerName$: any;
  playerName!: string;

  constructor(
    private saService: SocialAuthService,
    private uaService: UserAuthService
  ) {
    this.playerName$ = this.uaService.playerName$;
  }

  ngOnInit() {
    this.saService.authState.subscribe(user => {
      this.uaService.setPlayerName(user.firstName);
      console.log(`log in: ${user}`);
    });

    this.uaService.playerName$.subscribe(val => {
      this.playerName = val;
      console.log(`init: ${val}`);
    });
  }
}
