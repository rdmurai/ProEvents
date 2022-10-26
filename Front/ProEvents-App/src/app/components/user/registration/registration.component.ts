import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorPassword } from 'src/app/helpers/ValidatorPassword';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  form!:FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  get f(): any{
    return this.form.controls;
  }

  private validation():void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorPassword.MustMatch('password', 'confirmPassword')
    }

    this.form = this.fb.group(
      {
        firstName:['', Validators.required],
        lastName:['', Validators.required],
        email:['', [Validators.required, Validators.email]],
        username: ['', Validators.required],
        password:['', [Validators.required, Validators.minLength(6)]],
        confirmPassword:['', Validators.required]
      },
      formOptions
    )
  }

}
