import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppointmentService } from '../../services/appointment.service';
import { Appointment } from '../../models/appointment.model';
import { NavbarComponent } from '../../components/navbar/navbar.component';

@Component({
  selector: 'app-appointments',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css'],
})
export class AppointmentsComponent implements OnInit {
  appointments: Appointment[] = [];
  newAppointment: Appointment = {
    providerId: 0,
    clientId: 0,
    doctorName: '',
    date: '',
    time: '',
    status: 'Pending',
  };

  successMessage = '';
  errorMessage = '';

  constructor(private appointmentService: AppointmentService) {}

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments(): void {
    this.appointmentService.getAppointments().subscribe({
      next: (data) => (this.appointments = data),
      error: (err) => console.error('Error loading appointments:', err),
    });
  }

  addAppointment(): void {
    if (!this.newAppointment.doctorName || !this.newAppointment.date || !this.newAppointment.time) {
      this.errorMessage = 'Please fill all required fields.';
      return;
    }

    this.appointmentService.addAppointment(this.newAppointment).subscribe({
      next: (response) => {
        this.successMessage = 'Appointment booked successfully!';
        this.errorMessage = '';
        this.appointments.push(response);
        this.newAppointment = {
          providerId: 0,
          clientId: 0,
          doctorName: '',
          date: '',
          time: '',
          status: 'Pending',
        };
      },
      error: (err) => {
        this.errorMessage = 'Failed to book appointment. Try again.';
        console.error(err);
      },
    });
  }

  deleteAppointment(id: number): void {
    this.appointmentService.deleteAppointment(id).subscribe({
      next: () => {
        this.appointments = this.appointments.filter((a) => a.id !== id);
      },
      error: (err) => console.error('Error deleting appointment:', err),
    });
  }
}
