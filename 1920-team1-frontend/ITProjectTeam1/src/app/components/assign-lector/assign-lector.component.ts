import {Component, Inject, Input, OnInit} from '@angular/core';
import {LectorService} from '../../services/lector.service';
import {Lector} from '../../models/lector';
import {Role} from '../../enums/role.enum';
import {FormBuilder, FormControl} from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import {Internship} from '../../models/internship';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-assign-lector',
  templateUrl: './assign-lector.component.html',
  styleUrls: ['./assign-lector.component.scss']
})
export class AssignLectorComponent implements OnInit {
  internship: Internship;
  lectorControl = new FormControl();
  lectors: Lector[] = [];
  filteredLectors: Observable<Lector[]>;
  selectedLectors: Lector[] = [];
  selectedLectorIds: number[] = [];

  constructor(private dialogRef: MatDialogRef<AssignLectorComponent>,
              private ls: LectorService,
              private fb: FormBuilder,
              @Inject(MAT_DIALOG_DATA) private data) {
  }

  ngOnInit(): void {
    this.ls.getAll().subscribe((result: Lector[]) => {
      this.lectors = result;
    });
    this.internship = this.data.internship;

    this.filteredLectors = this.lectorControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  private _filter(value: string): Lector[] {
    if (typeof (value) === 'string') {
      const filterValue = value.toLowerCase();
      return this.lectors
        .filter(lector => lector.name.toLowerCase().includes(filterValue)
          || lector.firstName.toLowerCase().includes(filterValue));
    }
  }

  setValue(event: Lector): void {
    this.lectorControl.reset();
    this.lectors.splice(this.lectors.indexOf(event), 1);
    this.selectedLectors.push(event);
    this.selectedLectorIds.push(event.id);
  }

  assignLector(): void {
    this.dialogRef.close({id: this.internship.id, lectorIds: this.selectedLectorIds});
  }

  deleteLector(lector: Lector): void {
    this.selectedLectors.splice(this.selectedLectors.indexOf(lector), 1);
    this.selectedLectorIds.splice(this.selectedLectorIds.indexOf(lector.id), 1);
    this.lectors.push(lector);
    this.lectorControl.reset();
  }
}
