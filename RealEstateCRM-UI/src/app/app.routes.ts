import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./features/dashboard/dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [authGuard]
  },
  {
    path: 'customers',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./features/customers/customer-list/customer-list.component').then(m => m.CustomerListComponent)
      },
      {
        path: 'create',
        loadComponent: () => import('./features/customers/customer-form/customer-form.component').then(m => m.CustomerFormComponent)
      },
      {
        path: 'edit/:id',
        loadComponent: () => import('./features/customers/customer-form/customer-form.component').then(m => m.CustomerFormComponent)
      }
    ]
  },
  {
    path: 'leads',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./features/leads/lead-list/lead-list.component').then(m => m.LeadListComponent)
      },
      {
        path: 'create',
        loadComponent: () => import('./features/leads/lead-form/lead-form.component').then(m => m.LeadFormComponent)
      },
      {
        path: 'edit/:id',
        loadComponent: () => import('./features/leads/lead-form/lead-form.component').then(m => m.LeadFormComponent)
      }
    ]
  },
  {
    path: 'properties',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./features/properties/property-list/property-list.component')
            .then(m => m.PropertyListComponent)
      },
      {
        path: 'create',
        loadComponent: () =>
          import('./features/properties/property-form/property-form.component')
            .then(m => m.PropertyFormComponent)
      },
      {
        path: 'edit/:id',
        loadComponent: () =>
          import('./features/properties/property-form/property-form.component')
            .then(m => m.PropertyFormComponent)
      },
      {
        path: 'detail/:id',
        loadComponent: () =>
          import('./features/properties/property-detail/property-detail.component')
            .then(m => m.PropertyDetailComponent)
      },
    ]
  },
  {
    path: 'appointments',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/appointments/appointments-calendar.component')
        .then(m => m.AppointmentsCalendarComponent)
  },
  { path: '**', redirectTo: '/dashboard' }
];
