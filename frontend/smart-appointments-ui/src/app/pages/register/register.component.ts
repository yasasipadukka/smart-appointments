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
  name = '';
  email = '';
  password = '';

  constructor(private auth: AuthService, private router: Router) {}

  register() {
    if (this.name && this.email && this.password) {
      this.auth
        .register({ name: this.name, email: this.email, password: this.password })
        .subscribe({
          next: (res) => {
            alert('Registered successfully!');
            this.router.navigate(['/login']);
          },
          error: (err) => {
            console.error(err);
            alert('Registration failed.');
          },
        });
    } else {
      alert('Please fill all fields');
    }
  }
}
