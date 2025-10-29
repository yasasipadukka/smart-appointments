import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../components/navbar/navbar.component';

@Component({
  selector: 'app-specialists',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './specialists.component.html',
  styleUrls: ['./specialists.component.css'],
})
export class SpecialistsComponent {
  specialists = [
    { name: 'Dr. Smith', field: 'Cardiologist' },
    { name: 'Dr. Emily', field: 'Dermatologist' },
    { name: 'Dr. Kevin', field: 'Pediatrician' },
  ];
}
