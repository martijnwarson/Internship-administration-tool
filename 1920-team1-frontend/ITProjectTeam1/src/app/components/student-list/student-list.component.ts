import {Component, EventEmitter, Input, Output} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {NewStudentDialogComponent} from './new-student-dialog/new-student-dialog.component';
import {StudentService} from '../../services/student.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {filter} from 'rxjs/operators';
import {FileUploadDialogComponent} from '../file-upload-dialog/file-upload-dialog.component';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent {
  @Input() itemList: Array<any>;
  @Output() selectedItemId = new EventEmitter<string>();
  @Output() refresh = new EventEmitter<void>();

  constructor(private dialog: MatDialog, private ss: StudentService, private snackBar: MatSnackBar) {
  }

  showItem(id): void {
    this.selectedItemId.emit(id);
  }

  deleteStudent(id: number): void {
    if (id) {
      this.ss.deleteStudent(id).pipe(filter(Boolean)).subscribe(() => {
        this.refresh.emit();
        this.snackBar.open('Student is verwijderd', null, {
          duration: 3000
        });
      }, () => {
        this.snackBar.open('Er ging iets mis met het verwijderen van een student', null, {
          duration: 3000
        });
      });
    }
  }

  openCreateStudentDialog(): void {
    const dialogRef = this.dialog.open(NewStudentDialogComponent, {
      width: '800px',
      disableClose: true,
      data: {
        isLector: false
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.ss.createStudent(result).subscribe(() => {
          this.refresh.emit();
          this.snackBar.open('Student is aangemaakt', null, {
            duration: 3000
          });
        }, () => {
          this.snackBar.open('Er ging iets mis met het aanmaken van een student', null, {
            duration: 3000
          });
        });
      }
    });
  }

  uploadDialog(): void {
    const dialogRef = this.dialog.open(FileUploadDialogComponent, {
      width: '400px',
      disableClose: true
    });
    dialogRef.afterClosed().subscribe((res: File) => {
      if (res) {
        this.ss.uploadCsv(res).subscribe(result => {
            this.refresh.emit();
            this.snackBar.open('Studenten succesvol toegevoegd', null, {
              duration: 3000
            });
          },
          () => {
            this.refresh.emit();
            this.snackBar.open('Studenten succesvol toegevoegd', null, {
              duration: 3000
            });
          });
      }
    });
  }
}
