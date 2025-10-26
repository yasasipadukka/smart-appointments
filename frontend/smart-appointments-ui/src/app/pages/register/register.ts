import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.html',
})
export class RegisterComponent {
  user = { firstName: '', lastName: '', email: '', password: '', role: 'Client' };

  constructor(private authService: AuthService) {}

  onRegister() {
    this.authService.register(this.user).subscribe({
      next: (res) => alert('Registration successful!'),
      error: (err) => alert(err.error.message || 'Error during registration'),
    });
  }
}
