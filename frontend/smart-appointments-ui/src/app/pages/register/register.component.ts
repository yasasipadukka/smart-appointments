import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  firstName = '';
  lastName = '';
  email = '';
  password = '';

  constructor(private auth: AuthService, private router: Router) {}

  register() {
    if (this.firstName && this.lastName && this.email && this.password) {
      const userData = {
        firstName: this.firstName,
        lastName: this.lastName,
        email: this.email,
        password: this.password,
        role: 'Client', // optional but matches backend default
      };

      this.auth.register(userData).subscribe({
        next: (res) => {
          alert('✅ Registered successfully!');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.error('❌ Registration failed:', err);
          alert('Registration failed. Please check console.');
        },
      });
    } else {
      alert('Please fill all fields');
    }
  }
}
