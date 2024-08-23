import { Component, Input } from '@angular/core';
import { AsyncPipe, NgFor } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

import { Message } from '../../data/model';
import { AppSignalrService } from '../app-signalr.service';
import { Stat, Color } from '../../data/const';
import { UserAuthService } from '../user-auth.service';

@Component({
  selector: 'app-messaging',
  standalone: true,
  imports: [NgFor, AsyncPipe, ReactiveFormsModule ],
  templateUrl: './messaging.component.html',
  styleUrl: './messaging.component.css'
})
export class MessagingComponent {
  public get stat(): typeof Stat { return Stat; }
  public get color(): typeof Color { return Color; }
  get content(): any { return this.sendForm.get('Content'); }

  playerName = this.uaService.playerName;

  receivedMessages: Message[] = [];

  myMessage: Message = {
    Username: '',
    Content: ''
  }

  sendForm = this.fb.group({
    Content: ''
  });

  constructor(
    private signalRService: AppSignalrService,
    private uaService: UserAuthService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.playerName.subscribe(val => {
      if (val) {
        this.myMessage = {
          Username: val,
          Content: ''
        };
      }
    })
    
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveMessage().subscribe((message) => {
        this.receivedMessages.push(message);
      });
    });
  }

  changeUsername(newName: string) {
    this.myMessage.Username = newName;
  }

  sendSubmit() {
    this.myMessage.Content = this.sendForm.value.Content!;
    this.sendMessage(this.myMessage);
    this.content.reset();
  }

  sendMessage(message: Message) {
    this.signalRService.sendMessage(message);
  }
}
