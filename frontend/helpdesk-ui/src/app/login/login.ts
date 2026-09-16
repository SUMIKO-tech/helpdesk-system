import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
//testing
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html'
})
export class LoginComponent {
  email = '';
  password = '';
  error = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: (res) => {
        if (res.role === 'admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/employee']);
        }
      },
      error: () => {
        this.error = 'Invalid email or password.';
      }
    });
  }
}