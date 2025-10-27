import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email = '';
  password = '';

  constructor(private auth: AuthService, private router: Router) {}

  login() {
    if (this.email && this.password) {
      this.auth.login({ email: this.email, password: this.password }).subscribe({
        next: (res: any) => {
          alert('Login successful!');
          localStorage.setItem('token', res.token); // save JWT
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          console.error(err);
          alert('Login failed. Check your credentials.');
        },
      });
    } else {
      alert('Please fill all fields');
    }
  }
}
