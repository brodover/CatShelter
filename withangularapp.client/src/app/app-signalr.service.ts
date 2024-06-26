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
      .configureLogging(signalR.LogLevel.Information)
      .build();
  }
  
  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
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
      this.hubConnection.on('ReceiveMessage', (username, content) => {
        observer.next({
          Username: username,
          Content: content
        });
      });
    });
  }

  sendMessage(message: Message): void {
    if (this.hubConnection.state === signalR.HubConnectionState.Connected) {
      this.hubConnection.invoke('SendMessage', message.Username, message.Content)
        .catch(err => console.error(err));
    } else {
      console.error(`Cannot send data if the connection is not in the "Connected" state. Current state: ${this.hubConnection.state}`);
    }
  }
}
