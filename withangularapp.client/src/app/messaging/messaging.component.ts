import { Component } from '@angular/core';
import { NgFor } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

import { Message } from '../../data/model';
import { AppSignalrService } from '../app-signalr.service';
import { sendMessage } from '@microsoft/signalr/dist/esm/Utils';

@Component({
  selector: 'app-messaging',
  standalone: true,
  imports: [ NgFor, ReactiveFormsModule ],
  templateUrl: './messaging.component.html',
  styleUrl: './messaging.component.css'
})
export class MessagingComponent {
  myMessage: Message;
  receivedMessages: Message[] = [];

  sendForm = this.fb.group({
    Content: ''
  });

  constructor(
    private signalRService: AppSignalrService,
    private fb: FormBuilder
  ) {
    this.myMessage = {
      Username: new Date().toTimeString(),
      Content: ''
    };
  }

  ngOnInit() {
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
    this.sendForm.value.Content = '';
  }

  sendMessage(message: Message) {
    this.signalRService.sendMessage(message);
  }
}
