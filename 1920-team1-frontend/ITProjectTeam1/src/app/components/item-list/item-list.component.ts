import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TooltipPosition} from "@angular/material/tooltip";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss']
})
export class ItemListComponent {
  @Input() itemList: Array<any>;
  @Output() selectedItemId = new EventEmitter<string>();
  positionOptions: TooltipPosition[] = ['after', 'before', 'above', 'below', 'left', 'right'];
  position = new FormControl(this.positionOptions[5]);

  showItem(id): void {
    this.selectedItemId.emit(id);
  }
}
