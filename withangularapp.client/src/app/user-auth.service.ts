import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {
  private _playerName = new Subject<string>();
  public playerName$ = this._playerName.asObservable();

  constructor(
    private lsService: LocalStorageService
  ) { }
  public get playerName(): Observable<string | undefined> {

    return this.playerName$;

  }  
  getPlayerName() {
    var name = this.lsService.getItem('PlayerName');
    if (name == null) {
      name = new Date().getTime().toString();
      this.setPlayerName(name);
    }

    return name;
  }
  setPlayerName(name: string) {
    this._playerName.next(name);
    this.lsService.setItem('PlayerName', name);
  }
}
