import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  emailLoginForm = this.fb.group({
    Email: '',
    Pass: ''
  });

  constructor(
    private fb: FormBuilder
  ) { }

  loginGoogle() {

  }

  loginEmailSubmit() {

  }
}
