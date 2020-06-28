import {Component, OnInit} from '@angular/core';
import {Student} from '../../models/student';
import {StudentService} from '../../services/student.service';
import {finalize} from 'rxjs/operators';
import {NgxSpinnerService} from 'ngx-spinner';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.scss']
})
export class StudentsComponent implements OnInit {
  students: Student[];
  student: Student;
  isLoading: boolean;
  isLoadingDetails: boolean;

  constructor(private ss: StudentService, private spinner: NgxSpinnerService) {
  }

  ngOnInit(): void {
    this.getStudents();
  }

  getStudents(): void {
    this.spinner.show().then(() => this.isLoading = true);
    this.ss.getAll()
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.isLoading = false);
          }, 1000);
        })
      )
      .subscribe((result: Student[]) => {
        this.students = result;
      });
  }

  refresh(): void {
    this.getStudents();
  }

  showItem(event): void {
    this.spinner.show().then(() => this.isLoadingDetails = true);
    this.ss.getById(event)
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.spinner.hide().then(() => this.isLoadingDetails = false);
          }, 1000);
        })
      )
      .subscribe((result: Student) => {
        this.student = result;
      });
  }
}
