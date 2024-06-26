import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';

import { Message } from '../data/model';

@Injectable({
  providedIn: 'root'
})
export class AppSignalrService {
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/hub')
      .withAutomaticReconnect()
      .build();
  }

  startConnection(): Observable<void> {
    console.log("startConnection");
    return new Observable<void>((observer) => {
      console.log("start observe");
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  receiveMessage(): Observable<Message> {
    return new Observable<Message>((observer) => {
      this.hubConnection.on('ReceiveMessage', (message: Message) => {
        observer.next(message);
      });
    });
  }

  sendMessage(message: Message): void {
    this.hubConnection.invoke('SendMessage', message.Username, message.Content);
  }
}
