import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {InternshipService} from '../../services/internship.service';
import {forkJoin} from 'rxjs';
import {Course} from '../../models/course';
import {Technology} from '../../models/technology';
import {Period} from '../../models/period';
import {Internship} from '../../models/internship';
import {MatSnackBar} from '@angular/material/snack-bar';
import {STEPPER_GLOBAL_OPTIONS} from '@angular/cdk/stepper';
import {CompanyService} from '../../services/company.service';
import {NgxSpinnerService} from 'ngx-spinner';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-internship-form',
  templateUrl: './internship-form.component.html',
  styleUrls: ['./internship-form.component.scss'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: {showError: true}
    }
  ]
})
export class InternshipFormComponent implements OnInit {
  @ViewChild('firstForm') first;
  @ViewChild('secondForm') second;
  @ViewChild('thirdForm') third;
  @ViewChild('fourthForm') fourth;
  @ViewChild('fifthForm') fifth;
  @ViewChild('sixthForm') sixth;

  @Input() linear: boolean;
  @Input() internship: Internship;
  @Output() updated = new EventEmitter<boolean>();
  @Output() isLoading = new EventEmitter<boolean>();

  formGroup: FormGroup;
  contactGroup: FormGroup;
  promotorGroup: FormGroup;
  locationGroup: FormGroup;
  environmentGroup: FormGroup;
  extraFormGroup: FormGroup;
  conditionsControl: FormControl;
  researchTopic: FormControl;
  formsCreated = false;
  courses: Course[];
  technologies: Technology[];
  periods: Period[];
  loading = true;

  constructor(private fb: FormBuilder, private is: InternshipService, private snackBar: MatSnackBar, private spinner: NgxSpinnerService) {
  }

  ngOnInit(): void {
    this.isLoading.emit(true);
    forkJoin([
        this.is.getCourses(),
        this.is.getTechnologies(),
        this.is.getPeriods()
      ]
    )
      .pipe(
        finalize(() => {
          this.isLoading.emit(false);
          this.loading = false;
        })
      )
      .subscribe(([courses, technologies, periods]) => {
        this.courses = courses;
        this.technologies = technologies;
        this.periods = periods;
      });
    if (this.internship) {
      this.setFormsWithValues();
    } else {
      this.createForms();
    }
  }

  createForms(): void {
    this.contactGroup = this.fb.group({
      title: [null, Validators.required],
      lastName: [null, Validators.required],
      firstName: [null, Validators.required],
      tel: [null, Validators.required],
      email: [null, Validators.required]
    });
    this.promotorGroup = this.fb.group({
      title: [null, Validators.required],
      lastName: [null, Validators.required],
      firstName: [null, Validators.required],
      tel: [null, Validators.required],
      email: [null, Validators.required]
    });
    this.locationGroup = this.fb.group({
      supportEmployees: [null, Validators.required],
      street: [null],
      number: [null],
      box: [null],
      zip: [null],
      city: [null],
      country: [null]
    });
    this.formGroup = this.fb.group({
      courses: [null, Validators.required],
      title: [null, Validators.required],
      description: [null, Validators.required]
    });
    this.environmentGroup = this.fb.group({
      technologies: [null, Validators.required],
      techDescription: [null, Validators.required]
    });
    this.extraFormGroup = this.fb.group({
      period: [null, Validators.required],
      expectations: [null],
      nrOfStudents: [null, Validators.required],
      students: [null],
      remarks: [null]
    });
    this.researchTopic = new FormControl(null, Validators.required);
    this.conditionsControl = new FormControl(null);
    this.formsCreated = true;
  }

  setFormsWithValues(): void {
    this.contactGroup = this.fb.group({
      title: [this.internship.contactPerson.title, Validators.required],
      lastName: [this.internship.contactPerson.name, Validators.required],
      firstName: [this.internship.contactPerson.firstName, Validators.required],
      tel: [this.internship.contactPerson.telephoneNumber, Validators.required],
      email: [this.internship.contactPerson.email, Validators.required]
    });
    this.promotorGroup = this.fb.group({
      title: [this.internship.promotors[0].title, Validators.required],
      lastName: [this.internship.promotors[0].name, Validators.required],
      firstName: [this.internship.promotors[0].firstName, Validators.required],
      tel: [this.internship.promotors[0].telephoneNumber, Validators.required],
      email: [this.internship.promotors[0].email, Validators.required]
    });
    this.locationGroup = this.fb.group({
      supportEmployees: [this.internship.nrOfSupportEmployees, Validators.required],
      street: [this.internship.address.street, Validators.required],
      number: [this.internship.address.number, Validators.required],
      box: [this.internship.address.box],
      zip: [this.internship.address.zipCode, Validators.required],
      city: [this.internship.address.city, Validators.required],
      country: [this.internship.address.country, Validators.required]
    });
    this.formGroup = this.fb.group({
      // tslint:disable-next-line:max-line-length
      courses: [this.internship.courses.map(course => course.id), Validators.required], // nog te testen en uit commentaar te halen in template
      title: [this.internship.title, Validators.required],
      description: [this.internship.description, Validators.required]
    });
    this.environmentGroup = this.fb.group({
      technologies: [this.internship.technologies.map(technology => technology.id), Validators.required],
      techDescription: [this.internship.techDescription, Validators.required]
    });
    this.extraFormGroup = this.fb.group({
      period: [this.internship.periods.map(period => period.id), Validators.required],
      expectations: [this.checkExpectations()],
      nrOfStudents: [this.internship.studentAmount, Validators.required],
      students: [''], // TODO nog te implementeren
      remarks: [this.internship.remarks]
    });
    this.researchTopic = new FormControl(this.internship.researchTopic, Validators.required);
    this.conditionsControl = new FormControl(this.internship.conditions);
    this.formsCreated = true;
  }

  checkExpectations(): number[] {
    const application = this.internship.application ? 1 : undefined;
    const resume = this.internship.resumee ? 2 : undefined;
    const reimbursement = this.internship.reimbursement ? 3 : undefined;

    return [application, resume, reimbursement];
  }

  mapFormsToInternship(): Internship {
    const expectations = this.extraFormGroup.get('expectations').value;
    const internship: Internship = {
      id: this.internship ? this.internship.id : undefined,
      courseIds: this.formGroup.get('courses').value,
      technologyIds: this.environmentGroup.get('technologies').value,
      periodIds: this.extraFormGroup.get('period').value,
      contactPerson: {
        ...this.contactGroup.getRawValue(),
        name: this.contactGroup.get('lastName').value,
        telephoneNumber: this.contactGroup.get('tel').value
      },
      promotors: [
        {
          ...this.promotorGroup.getRawValue(),
          name: this.promotorGroup.get('lastName').value,
          telephoneNumber: this.promotorGroup.get('tel').value
        }
      ],
      title: this.formGroup.get('title').value,
      description: this.formGroup.get('description').value,
      address: {
        street: this.locationGroup.get('street').value,
        number: this.locationGroup.get('number').value,
        box: this.locationGroup.get('box').value,
        zipCode: this.locationGroup.get('zip').value,
        city: this.locationGroup.get('city').value,
        country: this.locationGroup.get('country').value
      },
      techDescription: this.environmentGroup.get('techDescription').value,
      researchTopic: this.researchTopic.value,
      remarks: this.extraFormGroup.get('remarks').value,
      studentAmount: this.extraFormGroup.get('nrOfStudents').value,
      application: expectations ? expectations.includes(1) : false,
      resumee: expectations ? expectations.includes(2) : false,
      reimbursement: expectations ? expectations.includes(3) : false,
      studentIds: null,
      nrOfSupportEmployees: this.locationGroup.get('supportEmployees').value
    };

    return internship;
  }

  resetForms(): void {
    this.first.resetForm();
    this.second.resetForm();
    this.third.resetForm();
    this.fourth.resetForm();
    this.fifth.resetForm();
    this.sixth.resetForm();
    this.researchTopic.reset();
    this.conditionsControl.reset();
  }

  submitInternship(stepper): void {
    this.isLoading.emit(true);
    const saveAbleInternship = this.mapFormsToInternship();
    if (this.internship) {
      this.is.updateInternship(saveAbleInternship)
        .pipe(
          finalize(() => this.isLoading.emit(false))
        )
        .subscribe(result => {
          this.snackBar.open('Update van stage-aanvraag aangevraagd.', null, {
            duration: 3000
          });
          this.resetForms();
          stepper.reset();
          this.updated.emit(true);
        }, () => this.snackBar.open('Er ging iets mis met het updaten van de aanvraag.', null, {
          duration: 3000
        })
      );
    } else {
      this.is.createInternship(saveAbleInternship)
        .pipe(
          finalize(() => this.isLoading.emit(false))
        )
        .subscribe(result => {
        this.snackBar.open('Stage is aangevraagd.', null, {
          duration: 3000
        });
        this.resetForms();
        stepper.reset();
      }, () => this.snackBar.open('Er ging iets mis bij het aanvragen van een stage.', null, {
        duration: 3000
      }));
    }
  }
}
