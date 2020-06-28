import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {InternshipService} from '../../../services/internship.service';
import {Course} from '../../../models/course';
import {filter} from 'rxjs/operators';

@Component({
  selector: 'app-new-student-dialog',
  templateUrl: './new-student-dialog.component.html',
  styleUrls: ['./new-student-dialog.component.scss']
})
export class NewStudentDialogComponent implements OnInit {
  courses: Course[];
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<NewStudentDialogComponent>, private is: InternshipService,
              @Inject(MAT_DIALOG_DATA) public data: { isLector: boolean }) { }



  ngOnInit(): void {
    this.is.getCourses().pipe(filter(Boolean)).subscribe((courses: Course[]) => {
      this.courses = courses;
    });
    this.firstFormGroup = this.fb.group({
      courseId: [null, Validators.required],
      title: [null, Validators.required],
      name: [null, Validators.required],
      firstName: [null, Validators.required],
      telephoneNumber: [null, Validators.required],
      email: [null, Validators.required]
    });
    this.secondFormGroup = this.fb.group({
      street: [null, Validators.required],
      number: [null, Validators.required],
      zipCode: [null, Validators.required],
      city: [null, Validators.required],
      country: [null, Validators.required]
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    // tslint:disable-next-line:ban-types
    const student: Object = {
      courseId: this.firstFormGroup.get('courseId').value,
      title: this.firstFormGroup.get('title').value,
      name: this.firstFormGroup.get('name').value,
      firstName: this.firstFormGroup.get('firstName').value,
      telephoneNumber: this.firstFormGroup.get('telephoneNumber').value,
      email: this.firstFormGroup.get('email').value,
      address: {
        street: this.secondFormGroup.get('street').value,
        number: this.secondFormGroup.get('number').value,
        zipCode: this.secondFormGroup.get('zipCode').value,
        city: this.secondFormGroup.get('city').value,
        country: this.secondFormGroup.get('country').value
      }
    };
    // tslint:disable-next-line:ban-types
    const lector: Object = {
      coursesIds: this.firstFormGroup.get('courseId').value,
      title: this.firstFormGroup.get('title').value,
      name: this.firstFormGroup.get('name').value,
      firstName: this.firstFormGroup.get('firstName').value,
      telephoneNumber: this.firstFormGroup.get('telephoneNumber').value,
      email: this.firstFormGroup.get('email').value,
      address: {
        street: this.secondFormGroup.get('street').value,
        number: this.secondFormGroup.get('number').value,
        zipCode: this.secondFormGroup.get('zipCode').value,
        city: this.secondFormGroup.get('city').value,
        country: this.secondFormGroup.get('country').value
      }
    };
    this.dialogRef.close(this.data.isLector ? lector : student);
  }
}
