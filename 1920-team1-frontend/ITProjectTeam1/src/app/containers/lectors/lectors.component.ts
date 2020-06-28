import { Component, OnInit } from '@angular/core';
import {LectorService} from '../../services/lector.service';
import {Lector} from '../../models/lector';
import {NgxSpinnerService} from 'ngx-spinner';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-lectors',
  templateUrl: './lectors.component.html',
  styleUrls: ['./lectors.component.scss']
})
export class LectorsComponent implements OnInit {
  lectors: Lector[];
  lector: Lector;
  isLoading = false;
  isLoadingDetails: boolean;

  constructor(private ls: LectorService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show().then(() => this.isLoading = true);
    this.getLectors();
  }

  refresh(): void {
    this.getLectors();
  }

  getLectors(): void {
    this.ls.getAll()
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.isLoading = false);
          }, 1000);
        })
      )
      .subscribe((result: Lector[]) => {
        this.lectors = result;
      });
  }

  showItem(event): void {
    this.spinner.show().then(() => this.isLoadingDetails = true);
    this.ls.getById(event)
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.isLoadingDetails = false);
          }, 1000);
        })
      )
      .subscribe((result: Lector) => {
      this.lector = result;
    }, error => {
        this.lector = {
          firstName: 'Eric',
          name: 'Zeer rustig',
          title: 'CEO',
          email: 'eric@brie.be',
          telephoneNumber: 'krijgt ge niet',
          courses: [],
          role: 2
        };
      });
  }
}
