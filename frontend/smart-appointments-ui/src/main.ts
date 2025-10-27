import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { importProvidersFrom } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { Routes } from '@angular/router';

import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './app/pages/login/login.component';
import { RegisterComponent } from './app/pages/register/register.component';
import { DashboardComponent } from './app/pages/dashboard/dashboard.component';

// Define your routes here
const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' }, // ✅ pathMatch must be "full"
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent },
];

// Bootstrap the standalone root component
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes), // provide routing
    importProvidersFrom(HttpClientModule, BrowserModule, FormsModule), // import modules your components need
  ],
}).catch((err) => console.error(err));
