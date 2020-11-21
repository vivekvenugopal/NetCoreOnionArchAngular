import { NgModule } from '@angular/core';


import { EmployeeModule } from './Employee/employee.module';
import { SharedModule } from '../Shared/shared.module';
import { MainLayoutComponent } from './SiteLayout/MainLayout/main-layout.component';
import { LoginPageComponent } from './Login/login-page/login-page.component';
import { UserListComponent } from './User/user-list/user-list.component';
import { LoginLayoutComponent } from './SiteLayout/login-layout/login-layout.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { EmployeeListComponent } from './Employee/employee-list/employee-list.component';

@NgModule({
  declarations: [
    MainLayoutComponent,
    LoginPageComponent,
    UserListComponent,
    LoginLayoutComponent
  ],
  imports: [
    SharedModule,
  ],
  providers: [
  ],
  exports: [
    EmployeeModule,
    MainLayoutComponent
  ],
  entryComponents: [EmployeeListComponent]
})
export class PageModule { }
