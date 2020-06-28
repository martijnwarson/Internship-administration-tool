import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LectorListComponent } from './lector-list.component';

describe('LectorListComponent', () => {
  let component: LectorListComponent;
  let fixture: ComponentFixture<LectorListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LectorListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LectorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
