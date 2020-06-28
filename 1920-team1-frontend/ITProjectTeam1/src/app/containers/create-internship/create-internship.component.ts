import {Component, OnInit} from '@angular/core';
import {NgxSpinnerService} from 'ngx-spinner';

@Component({
  selector: 'app-create-internship',
  templateUrl: './create-internship.component.html',
  styleUrls: ['./create-internship.component.scss']
})
export class CreateInternshipComponent implements OnInit {

  constructor(private spinner: NgxSpinnerService) {
  }

  ngOnInit(): void {
  }

  setSpinner(event): void {
    if (event) {
      this.spinner.show();
    } else {
      this.spinner.hide();
    }
  }
}
