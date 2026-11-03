import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../../core/services/auth';
import { Register as RegisterModel } from '../../models/auth.model';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  name = '';
  email = '';
  password = '';
  confirmPassword = '';
  phone = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  register(): void {

    // Check whether passwords match
    if (this.password !== this.confirmPassword) {
      alert('Password and Confirm Password do not match.');
      return;
    }

    const registerData: RegisterModel = {
      name: this.name,
      email: this.email,
      password: this.password,
      phone: this.phone
    };

    this.authService.register(registerData).subscribe({

      next: (response) => {
        alert('Registration successful');
        this.router.navigate(['/login']);
      },

      error: (error) => {
        console.error('Registration failed:', error);
        alert('Registration failed. Please check your details.');
      }

    });
  }
}