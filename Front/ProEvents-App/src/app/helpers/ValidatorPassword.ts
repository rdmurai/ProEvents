import { AbstractControl, FormGroup } from '@angular/forms';

export class ValidatorPassword {
  static MustMatch (password: string, confirmPassword: string): any {
    return (group: AbstractControl) => {
      const formGroup = group as FormGroup;
      const control = formGroup.controls[password];
      const matchingControl = formGroup.controls[confirmPassword];

      if(matchingControl.errors && !matchingControl.errors['mustMatch']){
        return null
      }

      if(control.value != matchingControl.value){
        matchingControl.setErrors({mustMatch:true});
      } else {
        matchingControl.setErrors(null);
      }

      return null;
    }

  }
}
