import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateEmployeeComponent } from '../Employee/create-employee/create-employee.component';
import { SharedModule } from '../../Shared/shared.module';
import { SkillDialogComponent } from './create-employee/skill-dialog.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';

@NgModule({
  declarations: [CreateEmployeeComponent, SkillDialogComponent, EmployeeListComponent],
  imports: [SharedModule],
  exports: [CreateEmployeeComponent, EmployeeListComponent],
  entryComponents: [SkillDialogComponent]
})
export class EmployeeModule { }
