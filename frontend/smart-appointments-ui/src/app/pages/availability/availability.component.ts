import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../components/navbar/navbar.component';

interface Availability {
  id?: number;
  date: string;
  startTime: string;
  endTime: string;
  isAvailable: boolean;
}

@Component({
  selector: 'app-availability',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './availability.component.html',
  styleUrls: ['./availability.component.css'],
})
export class AvailabilityComponent implements OnInit {
  availabilities: Availability[] = [];
  newSlot: Availability = { date: '', startTime: '', endTime: '', isAvailable: true };

  ngOnInit(): void {
    // In real app, fetch existing slots from backend API
    this.availabilities = [
      { id: 1, date: '2025-10-28', startTime: '09:00', endTime: '12:00', isAvailable: true },
      { id: 2, date: '2025-10-28', startTime: '14:00', endTime: '17:00', isAvailable: false },
    ];
  }

  addAvailability() {
    if (this.newSlot.date && this.newSlot.startTime && this.newSlot.endTime) {
      const newId = this.availabilities.length + 1;
      this.availabilities.push({ id: newId, ...this.newSlot });
      this.newSlot = { date: '', startTime: '', endTime: '', isAvailable: true };
    } else {
      alert('Please fill all fields');
    }
  }

  deleteAvailability(id: number | undefined) {
    if (!id) return;
    this.availabilities = this.availabilities.filter((slot) => slot.id !== id);
  }

  toggleAvailability(slot: Availability) {
    slot.isAvailable = !slot.isAvailable;
  }
}
