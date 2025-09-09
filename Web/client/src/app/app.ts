import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { inject } from '@angular/core';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div style="max-width: 300px; margin: auto; padding: 2rem; border: 1px solid #ccc; border-radius: 8px;">
      <h3>Login</h3>
      <input [(ngModel)]="username" placeholder="Username" class="form-control" />
      <input [(ngModel)]="password" placeholder="Password" type="password" class="form-control" />
      <button (click)="login()" class="btn btn-primary" style="margin-top: 1rem;">Login</button>
      <p *ngIf="message" style="margin-top:1rem;">{{ message }}</p>
    </div>
  `,
  providers: [AuthService]
})
export class LoginComponent {
  private authService = inject(AuthService);

  username = '';
  password = '';
  message = '';

  login() {
    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        this.message = 'Login successful!';
      },
      error: err => {
        console.error(err);
        this.message = 'Login failed';
      }
    });
  }
}
