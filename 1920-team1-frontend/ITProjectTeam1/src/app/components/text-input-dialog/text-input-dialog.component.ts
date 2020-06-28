import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-text-input-dialog',
  templateUrl: './text-input-dialog.component.html',
  styleUrls: ['./text-input-dialog.component.scss']
})
export class TextInputDialogComponent implements OnInit {
  value = '';
  feedBackFormControl = new FormControl();
  constructor(private dialogRef: MatDialogRef<TextInputDialogComponent>, @Inject(MAT_DIALOG_DATA) public data) { }

  ngOnInit(): void {
  }

  save(): void {
    this.dialogRef.close(this.feedBackFormControl.value);
  }

  cancel(): void {
    this.dialogRef.close();
  }

}
