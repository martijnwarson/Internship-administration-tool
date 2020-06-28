import {Component, OnInit} from '@angular/core';
import {Company} from '../../models/company';
import {CompanyService} from '../../services/company.service';
import {CompanyState} from '../../enums/company-state.enum';
import {MatSnackBar} from '@angular/material/snack-bar';
import {NgxSpinnerService} from 'ngx-spinner';
import {finalize} from 'rxjs/operators';
import {MatDialog} from '@angular/material/dialog';
import {TextInputDialogComponent} from '../../components/text-input-dialog/text-input-dialog.component';

@Component({
  selector: 'app-new-companies',
  templateUrl: './new-companies.component.html',
  styleUrls: ['./new-companies.component.scss']
})
export class NewCompaniesComponent implements OnInit {
  company: Company;
  companies: Company[] = [];
  newCompanies: Company[] = [];
  otherCompanies: Company[] = [];
  isLoading = false;
  loadingDetails = false;

  constructor(private cs: CompanyService,
              private snackBar: MatSnackBar,
              private spinner: NgxSpinnerService,
              public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.getCompanies();
  }


  getCompanies(): void {
    this.spinner.show().then(() => this.isLoading = true);
    this.cs.getAll()
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.isLoading = false);
          }, 1000);
        })
      )
      .subscribe((result: Company[]) => {
      this.companies = result;
      this.newCompanies = this.companies.filter(item => item.state === CompanyState.NEW);
      this.otherCompanies = this.companies.filter(item => item.state !== CompanyState.NEW);
    });
  }

  showItem(event): void {
    this.spinner.show().then(() => this.loadingDetails = true);
    this.cs.getById(event)
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.loadingDetails = false);
          }, 1000);
        })
      )
      .subscribe((result: Company) => {
      this.company = result;
    });
  }

  approveCompany(companyId: number): void {
    this.cs.updateAcceptedCompanyState(companyId, 1).subscribe(() => {
      this.getCompanies();
      this.company = undefined;
      this.snackBar.open('Bedrijf geaccepteerd', null,  {
        duration: 3000
      });
    });
  }

  rejectCompany(companyId: number): void {
    const dialogref = this.dialog.open(TextInputDialogComponent, {
      width: '400px',
      disableClose: true
    });
    dialogref.afterClosed().subscribe((res: string) => {
      if (res) {
        this.cs.updateCompanyState(companyId, 2, res).subscribe(() => {
          this.getCompanies();
          this.company = undefined;
          this.snackBar.open('Bedrijf NIET geaccepteerd', null,  {
            duration: 3000
          });
        });
      }
    });
  }
}
