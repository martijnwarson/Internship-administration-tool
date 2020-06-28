import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TooltipPosition} from '@angular/material/tooltip';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-new-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.scss']
})
export class NewCompanyListComponent {
  @Input() companyList: Array<any>;
  @Output() selectedCompanyId = new EventEmitter<string>();
  positionOptions: TooltipPosition[] = ['after', 'before', 'above', 'below', 'left', 'right'];
  position = new FormControl(this.positionOptions[5]);

  showCompany(id): void {
    this.selectedCompanyId.emit(id);
  }


}
