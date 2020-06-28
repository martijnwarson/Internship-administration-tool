import {Component, OnInit} from '@angular/core';
import {InternshipService} from '../../services/internship.service';
import {Internship} from '../../models/internship';
import {finalize} from 'rxjs/operators';
import {CompanyService} from '../../services/company.service';
import {NgxSpinnerService} from 'ngx-spinner';


@Component({
  selector: 'app-company-internships',
  templateUrl: './company-internships.component.html',
  styleUrls: ['./company-internships.component.scss']
})
export class CompanyInternshipsComponent implements OnInit {
  internships: Internship[];
  internship: Internship;
  isLoading = false;
  loadingDetails = false;
  updatingInternship = false;

  constructor(private is: InternshipService, private spinner: NgxSpinnerService) {
  }

  ngOnInit(): void {
    this.getInternships();
  }

  showItem(event): void {
    this.spinner.show().then(() => this.loadingDetails = true);
    this.is.getById(event)
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.loadingDetails = false);
          }, 1000);
        })
      )
      .subscribe((result: Internship) => {
        this.internship = result;
      });
  }

  updateInternship(): void {
    this.updatingInternship = true;
  }

  getInternships(): void {
    this.spinner.show().then(() => this.isLoading = true);
    this.is.getInternshipForLoggedInCompany()
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.isLoading = false);
          }, 1000);
        })
      )
      .subscribe((result: Internship[]) => {
        this.internships = result;
      });
  }

  showOverview(event): void {
    this.updatingInternship = false;
    this.getInternships();
    this.internship = undefined;
    this.internships = undefined;
  }

  setSpinner(event): void {
    if (event) {
      this.spinner.show();
    } else {
      this.spinner.hide();
    }
  }
}
