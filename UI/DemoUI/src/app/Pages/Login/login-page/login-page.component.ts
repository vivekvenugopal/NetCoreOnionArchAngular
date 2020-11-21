import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '@service/authentication.service';
import { first } from 'rxjs/operators';
import { AlertDialogComponent } from '@shared/PopupDialogs/alert-dialog/alert-dialog.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';
  loginError = '';

  constructor(
    private formBuilder: FormBuilder, private route: ActivatedRoute,
    private router: Router, private dialog: MatDialog, private _authService: AuthenticationService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
    // reset login status
    this._authService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/Employee';
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this._authService.login(this.f.username.value, btoa(this.f.password.value))
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.loginError = error.Message;
          this.loading = false;
        });
  }
  clear() {
    this.loginError = '';
    this.loginForm.reset();
  }
}
