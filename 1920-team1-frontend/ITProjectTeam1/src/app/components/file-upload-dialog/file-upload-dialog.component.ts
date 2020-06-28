import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-file-upload-dialog',
  templateUrl: './file-upload-dialog.component.html',
  styleUrls: ['./file-upload-dialog.component.scss']
})
export class FileUploadDialogComponent implements OnInit {
  @ViewChild('fileUpload', {static: false}) fileUpload: ElementRef;
  dragOver: boolean;
  form: FormGroup;
  droppedFile: File;

  get fileName(): string {
    const segments = this.form.get('file').value.split('\\');
    return segments[segments.length - 1];
  }

  constructor(
    public dialogRef: MatDialogRef<FileUploadDialogComponent>,
    private fb: FormBuilder,
  ) {
  }
  ngOnInit() {
    this.createForm();
  }

  createForm(): void {
    this.form = this.fb.group({
        file: [null, Validators.required]
      }
    );
  }
  onUploadClick(): File {
    if (this.form.valid) {
      return this.droppedFile ? this.droppedFile : this.fileUpload.nativeElement.files[0];
    }
  }

  onDragLeave(): void {
    this.dragOver = false;
  }

  onDragOver(event): void {
    this.dragOver = true;
    event.preventDefault();
  }

  onDrop(event): void {
    event.preventDefault();
    this.droppedFile = event.dataTransfer.files[0];
    console.log(event);
    this.form.controls.file.setValue(this.droppedFile.name);
    console.log(this.form.value);
  }
}
