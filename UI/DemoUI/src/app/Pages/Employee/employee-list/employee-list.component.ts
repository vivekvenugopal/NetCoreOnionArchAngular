import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Employee } from '@model/Employee';
import { EmployeeService } from '@service/employee.service';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { ConfirmationDialogComponent } from '../../../Shared/PopupDialogs/ConfirmDialog/ConfirmationDialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees : Employee[]= [];
  employeeColumn: string[] = ['employeeId', 'firstName', 'lastName', 'dateOfjoining', 'dob', 'delete'];
  dataSource: MatTableDataSource<Employee>;
  isLoading = true;
  constructor(private employeeService:EmployeeService, private dialog: MatDialog,private _router: Router) { }

  ngOnInit() {
    this.employeeService.getEmployees().subscribe(
      (data)  => {
        this.employees = data;
        this.dataSource = new MatTableDataSource(data);
        this.isLoading = false;
                },
      (error) => this.isLoading = false
    )
  }
  edit( id:number)
  {
    this._router.navigate(['Employee/Edit', id]);
  }
  delete(rowIndex:number, id:number){
    this.deleteConfirm(rowIndex, id);
  }
    deleteConfirm(rowIndex:number, id:number): void {
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        width: '350px',
        data: "Do you confirm the deletion of this employee?"
      });
      
    dialogRef.afterClosed().subscribe(result => {
        if(result) {
          this.employees.splice(rowIndex,1);
          this.dataSource = new MatTableDataSource<Employee>(this.employees);

          this.employeeService.deleteEmployee(id).subscribe();
        }
      });
    }
    
}


