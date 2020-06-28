import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {finalize} from 'rxjs/operators';
import {CompanyService} from '../../services/company.service';
import {NgxSpinnerService} from 'ngx-spinner';


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  matcher: MyErrorStateMatcher;
  isLoading = false;

  constructor(public fb: FormBuilder, private router: Router, private auth: AuthService,
              private snackBar: MatSnackBar, private spinner: NgxSpinnerService) {
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      email: [null, Validators.required],
      password: [null, Validators.required]
    });
    this.matcher = new MyErrorStateMatcher();
  }

  login(): void {
    this.spinner.show().then(() => this.isLoading = true);
    const formValue = this.form.value;
    if (formValue.email && formValue.password) {
      this.auth.login(formValue.email, formValue.password)
        .pipe(
          finalize(() => this.spinner.hide().then(() => this.isLoading = false)
          )
        )
        .subscribe(
          (token) => {
            console.log('Successful login');
            this.auth.handleAuthentication(token);
            this.router.navigate(['/home']);
          },
          (error => {
            this.snackBar.open('Username/password is wrong.', null, {
              duration: 3000
            });
          })
        );
    }
  }

  register(): void {
    this.router.navigate(['/register']);
  }
}
