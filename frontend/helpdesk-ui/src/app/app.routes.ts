import { Routes } from '@angular/router';
import { LoginComponent } from './login/login';
import { EmployeeDashboardComponent } from './employee-dashboard/employee-dashboard';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard';
import { authGuard } from './guards/auth.guard';
import { roleGuard } from './guards/role.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'employee',
    component: EmployeeDashboardComponent,
    canActivate: [authGuard, roleGuard('employee')]
  },
  {
    path: 'admin',
    component: AdminDashboardComponent,
    canActivate: [authGuard, roleGuard('admin')]
  }
];