import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm!: FormGroup;
  validationErrors: string[] = [];

  constructor(private authService: AuthService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      //confirmPassword: ['', [Validators.required]],
    })

    // this.registerForm.controls['password'].valueChanges.subscribe(() => {
    //   this.registerForm.controls['confirmPassword'].updateValueAndValidity();
    // })
  }
  
  // matchValues(matchTo: string): ValidatorFn {
  //   return (control: AbstractControl) => {
  //     return control?.value === (control?.parent?.controls as { [key: string]: AbstractControl })[matchTo] ?
  //       null : { isMatching: true }
  //   }
  // }

  register() {
    this.authService.register(this.registerForm.value).subscribe({
      next: (v) => console.log(v),
      error: (e) => this.validationErrors = e,
      complete: () => this.router.navigateByUrl('')
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
