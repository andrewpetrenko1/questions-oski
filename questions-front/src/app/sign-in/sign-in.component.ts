import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../interfaces/user';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.sass']
})
export class SignInComponent implements OnInit {

  signInForm = new FormGroup({
    login: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required)
  });

  public isInvalidCredentials = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  isValid(field: string) {
    return this.signInForm.get(field)!.invalid && this.signInForm.get(field)!.touched;
  }

  signInSubmit() {
    if(!this.signInForm.valid) {
      this.signInForm.markAllAsTouched();
      return;
    }
    this.signInForm.disable();

    let user: User = {
      id: null, 
      login: this.signInForm.get('login')?.value, 
      password: this.signInForm.get('password')?.value
    };

    this.authService.signIn(user).subscribe(_ => this.router.navigate(['']), err => {
      if (err.status === 422) {
        this.isInvalidCredentials = true;
        this.signInForm.enable();
      }
    });

  }

}
