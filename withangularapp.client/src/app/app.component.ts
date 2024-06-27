import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

import { Cat, Message } from '../data/model';
import { Color, Pattern, Stat } from '../data/const';
import { LocalStorageService } from './local-storage.service';
import { AppSignalrService } from './app-signalr.service';
import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';
import { MessagingComponent } from './messaging/messaging.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NgIf, NgFor, ReactiveFormsModule, WeatherForecastComponent, MessagingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  Color: typeof Color = Color;
  Pattern: typeof Pattern = Pattern;
  Stat: typeof Stat = Stat;

  adoptableCats: Cat[] = [];
  myCats: Cat[] = [];

  username: string = '';
  megaphone: Message = {
    Username: 'MEGAPHONE',
    Content: ''
  }

  showMyCat = false;
  showAdoptableCat = false;

  renameForm = this.fb.group({
    Name: ''
  });

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private signalRService: AppSignalrService,
    private localStorageService: LocalStorageService
  ) {}

  ngOnInit() {
    this.getUsername();
    this.getMyCats();
  }

  /**
   * Visit shelter and view 3 random cats 
   */
  visitCats() {
    this.http.get<Cat[]>('/api/Cats/Visit').subscribe({
      next: (result) => {
        this.adoptableCats = result
        this.showAdoptableCat = result.length > 0;
      },
      error: (error) => { console.error(error); },
    });
  }

  /**
   * Adopt one of the visited cats
   * @param cat
   */
  adopt(cat: Cat) {
    const index = this.adoptableCats.indexOf(cat);
    cat.AdopterId = this.username;
    this.http.post<any>('/api/Cats/Adopt', cat).subscribe({
      next: (result) => {
        this.getMyCats();
        this.adoptableCats.splice(index, 1);

        if (cat.Stats == Stat.Perfect || cat.Color == Color.Rainbow)
          this.sendMegaphone(cat);
      },
      error: (error) => { console.error(error); },
    });
  }

  /**
   * Get user's adopted cats
   */
  getMyCats() {
    this.http.get<Cat[]>(`/api/Cats/GetAdopterId/${this.username}`).subscribe({
      next: (result) => {
        result.forEach(cat => { cat._showButton = false; });
        this.myCats = result;
        this.showMyCat = result.length > 0;
      },
      error: (error) => { console.error(error); },
    });
  }

  /**
   * Rename user's cat name
   */
  rename(cat: Cat) {
    this.name.reset();
    this.myCats.forEach(aCat => { aCat._showButton = false; });
    cat._showButton = true;
  }
  get name(): any { return this.renameForm.get('Name'); }

  renameSubmit(cat: Cat) {
    cat._showButton = false;

    const body = {
      Id: cat.Id,
      Name: this.renameForm.value.Name
    }

    this.http.put<any>('/api/Cats/Rename', body).subscribe({
      next: (result) => {
        this.getMyCats();
      },
      error: (error) => { console.error(error); },
    });

  }

  renameCancel(cat: Cat) {
    cat._showButton = false;
  }

  /**
   * Delete user's cat
   */
  abandon(cat: Cat) {
    if (confirm(`Are you sure you want to abandon ${cat.Name}?`)) {
      this.http.delete<any>(`/api/Cats/Abandon/${cat.Id}`).subscribe({
        next: (result) => {
          this.getMyCats();
        },
        error: (error) => { console.error(error); },
      });
    }
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

  sendMegaphone(cat: Cat) {
    var looks = Pattern[cat.Pattern];
    if (cat.Color != Color.NA)
      looks = Color[cat.Color] + ' ' + looks;

    if (cat.Stats == Stat.Perfect) {
      this.megaphone.Content = `${cat.AdopterId} adopted a ${looks} cat with Perfect stats!`;
      if (cat.Color == Color.Rainbow)
        this.megaphone.Content += '!! Wow!';
    }
    else
      this.megaphone.Content = `${cat.AdopterId} adopted a ${looks} cat!`;
    this.signalRService.sendMessage(this.megaphone);
  }

  title = 'Cat Shelter';
}
