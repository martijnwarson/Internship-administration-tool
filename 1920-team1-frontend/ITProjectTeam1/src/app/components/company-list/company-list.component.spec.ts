import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCompanyListComponent } from './new-company-list.component';

describe('NewCompanyListComponent', () => {
  let component: NewCompanyListComponent;
  let fixture: ComponentFixture<NewCompanyListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewCompanyListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCompanyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
