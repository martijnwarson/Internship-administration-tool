import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentInternshipsComponent } from './student-internships.component';

describe('StudentInternshipsComponent', () => {
  let component: StudentInternshipsComponent;
  let fixture: ComponentFixture<StudentInternshipsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentInternshipsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentInternshipsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
