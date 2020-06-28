import {Component, OnInit} from '@angular/core';
import {Company} from '../../models/company';
import {CompanyService} from '../../services/company.service';
import {CompanyState} from '../../enums/company-state.enum';
import {finalize} from 'rxjs/operators';
import {NgxSpinnerService} from 'ngx-spinner';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  styleUrls: ['./companies.component.scss']
})
export class CompaniesComponent implements OnInit {
  company: Company;
  companies: Company[];
  newCompanies: Company[];
  activeCompanies: Company[];
  inactiveCompanies: Company[];
  isLoading = true;
  loadingDetails = false;
  clicked: boolean;

  constructor(private cs: CompanyService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show();
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
      this.activeCompanies = this.companies.filter(item => item.state === CompanyState.ACTIVE);
      this.inactiveCompanies = this.companies.filter(item => item.state === CompanyState.INACTIVE);
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

}
