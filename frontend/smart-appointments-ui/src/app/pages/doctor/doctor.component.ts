import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Doctor {
  name: string;
  specialty: string;
  location: string;
  image: string;
}

@Component({
  selector: 'app-doctor-directory',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css'],
})
export class DoctorComponent {
  doctors: Doctor[] = [
    {
      name: 'Dr. John Doe',
      specialty: 'Cardiologist',
      location: 'Colombo',
      image: '/assets/doc1.jpg',
    },
    {
      name: 'Dr. Sarah Lee',
      specialty: 'Dermatologist',
      location: 'Kandy',
      image: '/assets/doc2.jpg',
    },
    {
      name: 'Dr. Ravi Perera',
      specialty: 'Pediatrician',
      location: 'Galle',
      image: '/assets/doc3.jpg',
    },
  ];

  searchText = '';

  get filteredDoctors() {
    return this.doctors.filter(
      (d) =>
        d.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
        d.specialty.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }
}
