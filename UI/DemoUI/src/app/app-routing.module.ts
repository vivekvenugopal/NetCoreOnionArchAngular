import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateEmployeeComponent } from './Pages/Employee/create-employee/create-employee.component';
import { EmployeeListComponent } from './Pages/Employee/employee-list/employee-list.component';
import { LoginPageComponent } from './Pages/Login/login-page/login-page.component';
import { AuthGuard } from '@shared/gaurds/auth.gaurd';
import { UserListComponent } from './Pages/User/user-list/user-list.component';
import { MainLayoutComponent } from './Pages/SiteLayout/MainLayout/main-layout.component';
import { LoginLayoutComponent } from './Pages/SiteLayout/login-layout/login-layout.component';

const routes: Routes = [
  {
    path: 'Login',
    component: LoginLayoutComponent,
    children: [{ path: '', component: LoginPageComponent, pathMatch: 'full' }]
  },

  // App routes goes here here
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: 'Employee', component: EmployeeListComponent, canActivate: [AuthGuard] },
      { path: 'Employee/Create', component: CreateEmployeeComponent, canActivate: [AuthGuard] },
      { path: 'Employee/Edit/:id', component: CreateEmployeeComponent, canActivate: [AuthGuard] },
      { path: 'User', component: UserListComponent, canActivate: [AuthGuard] },
      { path: '', redirectTo: '', pathMatch: 'full', canActivate: [AuthGuard]  }]
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
