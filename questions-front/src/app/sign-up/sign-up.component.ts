import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../interfaces/user';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.sass']
})
export class SignUpComponent implements OnInit {

  signUpForm = new FormGroup({
    login: new FormControl(null, Validators.required),
    email: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required)
  });

  errors: {msg: string}[] = [];
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  isValid(field: string) {
    return this.signUpForm.get(field)!.invalid && this.signUpForm.get(field)!.touched;
  }

  signUpSubmit() {
    if(!this.signUpForm.valid) {
      this.signUpForm.markAllAsTouched();
      return;
    }
    this.signUpForm.disable();
    this.errors = [];

    let newUser: User = {
      id: null,
      login: this.signUpForm.get('login')?.value,
      email: this.signUpForm.get('email')?.value,
      password: this.signUpForm.get('password')?.value
    }
    this.authService.signUp(newUser).subscribe(_ => this.router.navigate(['']), err => {
      if(err.status === 409) {
        this.errors.push({msg: err.error});
        this.signUpForm.enable();
      }
    })
  }

}
