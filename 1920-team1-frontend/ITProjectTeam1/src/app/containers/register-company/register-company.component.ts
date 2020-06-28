import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MyErrorStateMatcher} from '../login/login.component';
import {Role} from '../../enums/role.enum';
import {CompanyService} from '../../services/company.service';

@Component({
  selector: 'app-register-company',
  templateUrl: './register-company.component.html',
  styleUrls: ['./register-company.component.scss']
})
export class RegisterCompanyComponent implements OnInit {
  form: FormGroup;
  matcher: MyErrorStateMatcher;

  constructor(public fb: FormBuilder, private router: Router, private cs: CompanyService, private snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      Name: [null, Validators.required],
      AmountOfEmployees: [null, Validators.required],
      AmountOfEmployeesIT: [null, Validators.required],
      Address: this.fb.group({
        Street: [null, Validators.required],
        Number: [null, Validators.required],
        ZipCode: [null, Validators.required],
        City: [null, Validators.required],
        Country: [null, Validators.required]
      }),
      ContactPerson: this.fb.group({
        Title: [null, Validators.required],
        Name: [null, Validators.required],
        FirstName: [null, Validators.required],
        Email: [null, Validators.required]
      })
    });
    this.matcher = new MyErrorStateMatcher();
  }

  cancel(): void {
    this.router.navigate(['/login']);
  }

  save(): void {
    this.cs.createCompany(this.form.getRawValue()).subscribe((res) => {
      if (res) {
        this.snackBar.open('Bedrijf succesvol aangevraagd', null,  {
          duration: 3000
        });
        this.router.navigate(['/login']);
      }
    });
  }
}
