import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentInternshipListComponent } from './student-internship-list.component';

describe('StudentInternshipListComponent', () => {
  let component: StudentInternshipListComponent;
  let fixture: ComponentFixture<StudentInternshipListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentInternshipListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentInternshipListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
