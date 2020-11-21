import { FormControl, AbstractControl, FormGroup, FormGroupDirective, NgForm } from '@angular/forms'
import { ErrorStateMatcher } from '@angular/material';

export class CustomValidators   {
    
    static emailDomainValidator(domainName: string) {
        return (control: AbstractControl): { [key: string]: any } | null => {
            const email: string = control.value;
            const domain = email && email.substring(email.lastIndexOf('@') + 1);
            if (email === '' || domain === domainName)
                return null;
            else
                return { 'emailDomain': true };
        }
    }
    static todaysDateValidator() {
        return (control: AbstractControl): { [key: string]: any } | null => {
            const selectedDate: Date = control.value;
            const todaysDate: Date = new Date();
            if (selectedDate > todaysDate)
                return { 'todaysDate': true };
            else
                return null;
        }
    }
    static dateCompare() {
        return (group: FormGroup): { [key: string]: any } | null => {
            let firstDate: Date;
            let secondDate: Date;
            let index: number = 0;
            Object.keys(group.controls).forEach(key => {
                index = index + 1;
                if (index == 1) 
                    firstDate = group.get(key).value;
                else (index == 2)
                    secondDate = group.get(key).value;

            });
            if (secondDate  && firstDate > secondDate)
                return { 'dateCompareInvalid': true };
            else
                return null;
        }
    }
   
}

// This class is added from Angular Material for cross field validation
/** Error when the parent is invalid */
export class CrossFieldErrorMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
      return control.dirty && form.invalid;
    }
  }