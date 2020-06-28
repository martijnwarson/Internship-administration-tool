import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {filter} from 'rxjs/operators';
import {LectorService} from '../../services/lector.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {NewStudentDialogComponent} from '../student-list/new-student-dialog/new-student-dialog.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-lector-list',
  templateUrl: './lector-list.component.html',
  styleUrls: ['./lector-list.component.scss']
})
export class LectorListComponent {
  @Input() itemList: Array<any>;
  @Output() selectedItemId = new EventEmitter<string>();
  @Output() refresh = new EventEmitter<void>();

  constructor(private dialog: MatDialog, private ls: LectorService, private snackBar: MatSnackBar) {
  }

  showItem(id): void {
    this.selectedItemId.emit(id);
  }

  deleteLector(id: number): void {
    if (id) {
      this.ls.deleteLector(id).pipe(filter(Boolean)).subscribe(() => {
        this.refresh.emit();
        this.snackBar.open('Lector is verwijderd', null, {
          duration: 3000
        });
      }, () => {
        this.snackBar.open('Er ging iets mis met het verwijderen van een lector', null, {
          duration: 3000
        });
      });
    }
  }

  openCreateLectorDialog(): void {
    const dialogRef = this.dialog.open(NewStudentDialogComponent, {
      width: '800px',
      disableClose: true,
      data: {
        isLector: true
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.ls.createLector(result).subscribe(() => {
          this.refresh.emit();
          this.snackBar.open('Lector is aangemaakt', null, {
            duration: 3000
          });
        }, () => {
          this.snackBar.open('Er ging iets mis met het aanmaken van een lector', null, {
            duration: 3000
          });
        });
      }
    });
  }
}
