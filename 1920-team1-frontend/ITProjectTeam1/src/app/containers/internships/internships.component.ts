import {AfterContentChecked, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {InternshipService} from '../../services/internship.service';
import {Internship} from '../../models/internship';
import {InternshipState} from '../../enums/internship-state.enum';
import {MatDialog} from '@angular/material/dialog';
import {AssignLectorComponent} from '../../components/assign-lector/assign-lector.component';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Validation} from '../../models/validation';
import {FormControl} from '@angular/forms';
import {TextInputDialogComponent} from '../../components/text-input-dialog/text-input-dialog.component';
import {ValidationState} from '../../enums/validation-state.enum';
import {finalize} from 'rxjs/operators';
import {NgxSpinnerService} from 'ngx-spinner';
import {Feedback} from '../../models/feedback';

@Component({
  selector: 'app-internships',
  templateUrl: './internships.component.html',
  styleUrls: ['./internships.component.scss']
})
export class InternshipsComponent implements OnInit, AfterContentChecked {
  internships: Internship[];
  internship: Internship;
  newInterships: Internship[];
  otherInterships: Internship[];
  validations: Validation[];
  setState: boolean;
  loggedInAsLector = false;
  validationId: number;
  states = new FormControl();
  feedbackFormControl = new FormControl();
  role: string;
  isLoading = false;
  isLoadingDetails = false;
  isUpdating = false;
  statesList: any[] = [
    {name: 'In behandeling', id: 1},
    {name: 'Dient aangepast te worden', id: 2},
    {name: 'Goedgekeurd', id: 3},
    {name: 'Aangepast door bedrijf', id: 4},
    {name: 'Afgekeurd', id: 5}];
  filterInternships: Internship[] = [];
  show = false;
  feedBack: Feedback;
  feedBackId: number;

  constructor(private is: InternshipService,
              public dialog: MatDialog,
              private snackBar: MatSnackBar,
              private spinner: NgxSpinnerService,
              private cd: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.spinner.show().then(() => this.isLoading = true);
    this.role = localStorage.getItem('role');
    if (this.role === 'COÖRDINATOR') {
      this.is.getAll()
        .pipe(
          finalize(() => {
            setTimeout(() => {
              this.spinner.hide().then(() => this.isLoading = false);
            }, 1000);
          })
        )
        .subscribe((result: Internship[]) => {
          this.setInternshipsArrays(result);
        });
    } else if (this.role === 'LECTOR') {
      this.is.getInternshipForLoggedInLector()
        .pipe(
          finalize(() => {
            setTimeout(() => {
              this.spinner.hide().then(() => this.isLoading = false);
            }, 1000);
          })
        )
        .subscribe((result: Internship[]) => {
          this.internships = result;
          this.otherInterships = result;
          this.loggedInAsLector = true;
        });
    }
    this.states.valueChanges.subscribe(result => {
      this.filterInternships.length = 0;
      const other = this.internships.filter(item => item.state !== InternshipState.NEW);
      const internships = [...other];
      if (result.length) {
        result.forEach(state => {
          const foundInternShips = internships.filter(internship => internship.state === state);
          foundInternShips.forEach(val => this.filterInternships.push(val));
        });
        this.otherInterships = this.filterInternships;
      } else {
        this.otherInterships = other;
      }
    });

  }

  showItem(event): void {
    this.spinner.show().then(() => this.isLoadingDetails = true);
    this.validationId = null;
    this.show = false;
    this.feedbackFormControl.reset();
    this.is.getById(event).subscribe((result: Internship) => {
      this.internship = result;
    });
    if (this.role === 'COÖRDINATOR') {
      this.is.getValidationsById(event)
        .pipe(
          finalize(() => {
            setTimeout(() => {
              this.spinner.hide().then(() => this.isLoadingDetails = false);
            }, 1000);
          })
        )
        .subscribe((result: Validation[]) => {
          this.validations = result;
        });
    } else if (this.role === 'LECTOR') {
      this.is.getValidationsByLector(event)
        .pipe(
          finalize(() => {
            setTimeout(() => {
              this.spinner.hide().then(() => this.isLoadingDetails = false);
            }, 1000);
          })
        )
        .subscribe((result: Validation[]) => {
          const feedback = result[0].feedBack;
          this.validations = result;
          this.validationId = result[0].id;
          if (feedback) {
            this.feedBackId = feedback.id;
            this.feedbackFormControl.setValue(result[0].feedBack.value);
          }
        });
    }
  }

  showDialog(): void {
    this.setState = true;
    const dialogRef = this.dialog.open(AssignLectorComponent, {
      width: '400px',
      disableClose: true,
      data: {
        internship: this.internship
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinner.show().then(() => this.isLoading = true);
        this.is.createValidations(result.id, result.lectorIds)
          .pipe(
            finalize(() => {
              setTimeout(() => {
                this.spinner.hide().then(() => this.isLoading = false);
              }, 1000);
            })
          ).subscribe(() => {
          this.snackBar.open('Stageaanvraag is in behandeling', null, {
            duration: 3000
          });
        }, error => {
          this.snackBar.open('', null, {
            duration: 3000
          });
        });
      }
    });
  }

  showFeedBack(selectedValidation: Validation): void {
    this.feedBack = selectedValidation.feedBack;
    this.show = true;
  }

  approve(): void {
    this.spinner.show().then(() => this.isLoading = true);
    this.resetScreen();
    this.is.updateApprovedState(this.internship, 3).subscribe(result => {
      this.is.getAll().subscribe(internships => {
        this.internship = undefined;
        this.setInternshipsArrays(internships);
        this.spinner.hide().then(() => {
          this.isLoading = false;
          this.snackBar.open('Stageaanvraag is goedgekeurd.', null, {
            duration: 3000
          });
        });
      });
    });
  }

  reject(): void {
    const dialogref = this.dialog.open(TextInputDialogComponent, {
      width: '400px',
      disableClose: true
    });
    dialogref.afterClosed().subscribe((res: string) => {
      if (res) {
        this.spinner.show().then(() => this.isLoading = true);
        this.resetScreen();
        this.is.updateState(this.internship, 5, res).subscribe(result => {
          this.is.getAll().subscribe(internships => {
            this.internship = undefined;
            this.setInternshipsArrays(internships);
            this.spinner.hide().then(() => {
              this.isLoading = false;
              this.snackBar.open('Stageaanvraag is afgewezen.', null, {
                duration: 3000
              });
            });
          });
        });
      }
    });
  }

  toModify(): void {
    const dialogref = this.dialog.open(TextInputDialogComponent, {
      width: '400px',
      disableClose: true
    });
    dialogref.afterClosed().subscribe((res: string) => {
      if (res) {
        this.spinner.show().then(() => this.isLoading = true);
        this.resetScreen();
        this.is.updateState(this.internship, 2, res).subscribe(result => {
          this.is.getAll().subscribe(internships => {
            this.internship = undefined;
            this.setInternshipsArrays(internships);
            this.spinner.hide().then(() => {
              this.isLoading = false;
              this.snackBar.open('Stageaanvraag teruggestuurd naar bedrijf voor meer informatie.', null, {
                duration: 3000
              });
            });
          });
        });
      }
    });
  }

  updateValidation(state: number): void {
    this.spinner.show().then(() => this.isUpdating = true);
    const saveAbleValidation = {
      id: this.validationId,
      feedBack: this.mapToFeedBack(),
      state
    };
    this.is.updateValidation(this.validationId, this.internship.id, saveAbleValidation)
      .pipe(
        finalize(() => this.spinner.hide().then(() => this.isUpdating = false))
      )
      .subscribe(() => {
        if (state === 1) {
          this.snackBar.open('Stageaanvraag goedgekeurd.', null, {
            duration: 3000
          });
        } else if (state === 2) {
          this.snackBar.open('Stageaanvraag teruggestuurd voor aanpassing.', null, {
            duration: 3000
          });
        } else if (state === 3) {
          this.snackBar.open('Stageaanvraag afgewezen.', null, {
            duration: 3000
          });
        }
      });
  }

  mapToFeedBack(): Feedback {
    return {
      value: this.feedbackFormControl.value
    };
  }

  ngAfterContentChecked(): void {
    this.cd.detectChanges();
  }

  resetScreen(): void {
    this.internships = undefined;
    this.newInterships = undefined;
    this.otherInterships = undefined;
    this.validations = undefined;
  }

  setInternshipsArrays(internships: Internship[]): void {
    this.internships = internships;
    this.newInterships = internships.filter(item => item.state === InternshipState.NEW);
    this.otherInterships = internships.filter(item => item.state !== InternshipState.NEW);
  }
}
