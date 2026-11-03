import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../core/services/auth';
import { Login as LoginModel } from '../../models/auth.model';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  email = '';
  password = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  login(): void {

    const loginData: LoginModel = {
      email: this.email,
      password: this.password
    };

    this.authService.login(loginData).subscribe({

      next: (response) => {
        sessionStorage.setItem('token', response.token);

        alert('Login successful');

        this.router.navigate(['/movies']);
      },

      error: (error) => {
        console.error('Login failed:', error);
        alert('Invalid email or password');
      }

    });
  }
}