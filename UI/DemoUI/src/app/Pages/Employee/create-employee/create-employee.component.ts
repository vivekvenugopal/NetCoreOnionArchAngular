import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { MatDialog, MatTableDataSource, MatDialogRef, MAT_DIALOG_DATA, MatPaginator } from '@angular/material';
import { SkillDialogComponent } from './skill-dialog.component';
import { Employee, SkillSet } from '../../../model/Employee';
import { ValidatorErrorMessage } from '../../../Shared/Validators/ValidatorErrorMessage';
import { CustomValidators } from '../../../Shared/Validators/CustomValidator';
import { EmployeeService } from '../../../Service/employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationDialogComponent } from '../../../Shared/PopupDialogs/ConfirmDialog/ConfirmationDialog.component';
import { AlertDialogComponent } from '../../../Shared/PopupDialogs/alert-dialog/alert-dialog.component';


@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {

  employeeDetail: Employee;
  @ViewChild('stepper') stepper;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  technology: string;
  skillfromDate: Date;
  skilltoDate?: Date;
  skillsColumn: string[] = ['technology', 'fromDate', 'toDate', 'remove'];
  skillsArray: SkillSet[] = [];


  basicDetailsFormGroup: FormGroup;
  PIFormGroup: FormGroup;

  dataSource = new MatTableDataSource<SkillSet>(this.skillsArray);

  constructor(private _formBuilder: FormBuilder, private dialog: MatDialog,
              private changeDetectorRefs: ChangeDetectorRef, private _router: Router,
              private employeeService: EmployeeService, private route: ActivatedRoute) {

    this.employeeDetail = new Employee();
  }

  ngOnInit() {

    this.basicDetailsFormGroup = this._formBuilder.group({
      employeeId: ['', [Validators.required, Validators.maxLength(50)]],
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email, CustomValidators.emailDomainValidator('xyz.com')]],
      doj: ['', [Validators.required, CustomValidators.todaysDateValidator()]]
    });

    this.PIFormGroup = this._formBuilder.group({
      address: '',
      dob: ''
    });

    this.isEditEmployee();

  }
  isEditEmployee() {
    // route activated for edit
    this.route.paramMap.subscribe(params => {
      const empId = +params.get('id');
      if (empId) {
        this.employeeService.getEmployee(empId)
          .subscribe(
            (employee: Employee) => this.editEmployee(employee)
          );
      }
    });
  }
  editEmployee(employee: Employee) {
    this.employeeDetail.Id = employee.Id;
    this.basicDetailsFormGroup.patchValue({
      employeeId: employee.EmployeeId,
      firstName: employee.FirstName,
      lastName: employee.LastName,
      email: employee.Email,
      doj: employee.DOJ
    });
    this.PIFormGroup.patchValue({
      address: employee.Address,
      dob: employee.DOB
    });

    employee.Skills.forEach(skill => {
      this.skillsArray.push({
        Id: skill.Id,
        Technology: skill.Technology,
        StartDate: skill.StartDate,
        EndDate: skill.EndDate
      });
    });
    this.dataSource = new MatTableDataSource<SkillSet>(this.skillsArray);
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(SkillDialogComponent, {
      width: '400px',
      data: {
        technology: '',
        skillfromDate: null,
        skilltoDate: null
      }
    });

    dialogRef.afterClosed().subscribe(skillResult => {
      if (skillResult.Technology && skillResult.skillfromDate) {
        this.skillsArray.push({
          Id: 0,
          Technology: skillResult.Technology,
          StartDate: skillResult.skillfromDate,
          EndDate: skillResult.skilltoDate
        });
        this.dataSource.data = this.skillsArray;
      }
    });
  }
  removeSkill(rowIndex: number) {

    this.skillsArray.splice(rowIndex, 1);
    this.dataSource = new MatTableDataSource<SkillSet>(this.skillsArray);

  }
  onSubmit() {
    this.employeeDetail.EmployeeId = this.basicDetailsFormGroup.value.employeeId;
    this.employeeDetail.FirstName = this.basicDetailsFormGroup.value.firstName;
    this.employeeDetail.LastName = this.basicDetailsFormGroup.value.lastName;
    this.employeeDetail.Email = this.basicDetailsFormGroup.value.email;
    this.employeeDetail.DOJ = this.basicDetailsFormGroup.value.doj;
    this.employeeDetail.Address = this.PIFormGroup.value.address;
    this.employeeDetail.DOB = this.PIFormGroup.value.dob;
    this.employeeDetail.Skills = this.skillsArray;
    if (this.employeeDetail.Id && this.employeeDetail.Id > 0) {
      this.employeeService.updateEmployee(this.employeeDetail).subscribe(
        response => this.onEmployeeUpdateSuccess(response),
        err => console.log(err)
      );
    } else {
      this.employeeService.addEmployee(this.employeeDetail).subscribe(
        response => this.onEmployeeCreateSuccess(response),
        err => {
          if (err) {
           this.handleServerError(err);
          }
        }
      );

    }
  }
  handleServerError(err: any) {
    const controlList = Object.keys(err);
    controlList.forEach(element => {
      const control = this.basicDetailsFormGroup.controls[element];
      const message = err[element];
      control.setErrors({ serverError: message[0]  });
    });
    this.stepper.selectedIndex = 0;
  }
  onReset() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: 'Do you want to reset the data you have entered?'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.stepper.reset();
      }
    });
  }
  onEmployeeUpdateSuccess(employee: Employee) {
    const dialogRef = this.dialog.open(AlertDialogComponent, {
      width: '350px',
      data: 'The employee details have been updated successfully.'
    });
    setTimeout(() => {
      dialogRef.close();
      this._router.navigate(['Employee']);
    }, 2000);
  }
  onEmployeeCreateSuccess(employee: Employee) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: 'The employee has been created successfully. Do you wish to add more employees?'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.stepper.reset();
      } else {
        this._router.navigate(['Employee']);
      }
    });
  }

  getErrorMessage(control: AbstractControl, field: string) {
    return ValidatorErrorMessage.getErrorMessage(control, field);
  }
}
