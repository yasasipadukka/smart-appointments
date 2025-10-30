import { Routes } from '@angular/router';
import { HeroComponent } from './components/hero/hero.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AppointmentsComponent } from './pages/appointments/appointments.component';
import { AvailabilityComponent } from './pages/availability/availability.component';
import { SpecialistsComponent } from './pages/specialists/specialists.component';
import { DoctorComponent } from './pages/doctor/doctor.component';

export const routes: Routes = [
  { path: '', component: HeroComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'appointments', component: AppointmentsComponent },
  { path: 'availability', component: AvailabilityComponent },
  { path: 'specialists', component: SpecialistsComponent },
  { path: 'doctors', component: DoctorComponent },
];
