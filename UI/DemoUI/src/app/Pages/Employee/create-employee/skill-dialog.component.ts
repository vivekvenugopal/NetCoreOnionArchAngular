import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialogClose, MatDialogRef, MAT_DIALOG_DATA, ErrorStateMatcher, MatPaginator } from '@angular/material'
import { FormGroup, FormBuilder,FormControl, Validators, AbstractControl, FormGroupDirective, NgForm } from '@angular/forms';
import { SkillSet } from '../../../model/Employee';
import { ValidatorErrorMessage } from '../../../Shared/Validators/ValidatorErrorMessage';
import { CustomValidators, CrossFieldErrorMatcher } from '../../../Shared/Validators/CustomValidator';


@Component({
  selector: 'app-skill-dialog',
  templateUrl: './skill-dialog.component.html',
  styleUrls: []
})
export class SkillDialogComponent {

  skillForm: FormGroup;
  Technology: string;
  StartDate: Date;
  EndDate: Date;
  errorMatcher = new CrossFieldErrorMatcher();
  
  
  constructor(fb: FormBuilder, public dialogRef: MatDialogRef<SkillDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SkillSet) {
    this.Technology = data.Technology;
    this.StartDate = data.StartDate;
    this.EndDate = data.EndDate;
    this.skillForm = fb.group({
      Technology: ['', Validators.required],
      dateGroup: fb.group({ StartDate: ['', Validators.required], EndDate: ''},
                            {validators: CustomValidators.dateCompare()}) 
    });
  }

  onNoClick(): void {
      this.dialogRef.close();
  }
  saveSkill() {
    const { value, valid } = this.skillForm;
    if (valid) {
      this.dialogRef.close({ Technology: value.Technology, skillfromDate: value.dateGroup.StartDate,
        skilltoDate: value.dateGroup.EndDate});
    }
  }
  getErrorMessage(control: AbstractControl, field: string)
  {
     return ValidatorErrorMessage.getErrorMessage(control, field);
  }
}
