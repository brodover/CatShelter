import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {
  email = 'brodover@gmail.com';
  copied = 'Copied!';
  tooltip = this.email;
  visibility: string;

  constructor() {
    this.visibility = 'hidden';
  }

  copyText() {
    navigator.clipboard.writeText(this.email);
    this.tooltip = this.copied;
    this.setVisibility(true);

    setTimeout(() => {
      this.resetTooltip();
    }, 2500);
  }

  prepTooltip() {
    this.tooltip = this.email;
  }

  resetTooltip() {
    this.setVisibility(false);
  }

  setVisibility(visible: boolean) {
    if (visible)
      this.visibility = 'visible';
    else
      this.visibility = 'hidden';

    this.visibility += '!important';
  }
}
