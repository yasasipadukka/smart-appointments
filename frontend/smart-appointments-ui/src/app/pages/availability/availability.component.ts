import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../components/navbar/navbar.component';

@Component({
  selector: 'app-availability',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './availability.component.html',
  styleUrls: ['./availability.component.css'],
})
export class AvailabilityComponent {
  availableSlots = [
    { doctor: 'Dr. John', time: '9:00 AM - 11:00 AM' },
    { doctor: 'Dr. Lisa', time: '1:00 PM - 3:00 PM' },
  ];
}
