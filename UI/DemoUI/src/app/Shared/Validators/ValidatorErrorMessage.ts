import { AbstractControl } from '@angular/forms'
export abstract class ValidatorErrorMessage {

  public static getErrorMessage(control: AbstractControl, controlName: string ) {
    if (control.hasError('required'))
      return 'You must enter a value for ' + controlName+' field';
    else if (control.hasError('email'))
      return 'Please enter a valid mail Id '
    else if (control.hasError('emailDomain'))
      return 'Please enter the Organization mail Id';
    else if (control.hasError('emailDomain'))
      return 'The date cannot be greater than todays Date';
    else if (control.hasError('todaysDate'))
      return 'Please enter a date less than todays date.';
    else if (control.hasError('dateCompareInvalid'))
      return 'Please enter a date value less than '+ controlName+' field';
    else if (control.hasError('serverError'))
      return control.errors.serverError;
    else
      return '';

  }
}