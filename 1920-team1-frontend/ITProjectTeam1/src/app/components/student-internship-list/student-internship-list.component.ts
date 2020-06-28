import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TooltipPosition} from '@angular/material/tooltip';
import {FormControl} from '@angular/forms';
import {InternshipService} from '../../services/internship.service';

@Component({
  selector: 'app-student-internship-list',
  templateUrl: './student-internship-list.component.html',
  styleUrls: ['./student-internship-list.component.scss']
})
export class StudentInternshipListComponent {
  @Input() itemList: Array<any>;
  @Output() selectedItemId = new EventEmitter<string>();
  @Output() refresh = new EventEmitter<void>();
  positionOptions: TooltipPosition[] = ['after', 'before', 'above', 'below', 'left', 'right'];
  position = new FormControl(this.positionOptions[5]);

  constructor(private is: InternshipService) {}

  showItem(id): void {
    this.selectedItemId.emit(id);
  }

  markAsFavorite(id): void {
    this.is.markAsFavorite(id).subscribe(  () => this.refresh.emit());
  }

  removeAsFavorite(id): void {
    this.is.markAsFavorite(id).subscribe(() => this.refresh.emit());
  }
}
