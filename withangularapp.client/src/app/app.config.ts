import { ApplicationConfig } from '@angular/core';
import { provideHttpClient } from "@angular/common/http";
import { provideOAuthClient } from 'angular-oauth2-oidc';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    provideOAuthClient()
  ]
};
