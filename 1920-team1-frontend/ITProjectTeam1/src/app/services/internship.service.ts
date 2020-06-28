import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Internship} from '../models/internship';
import {Validation} from '../models/validation';
import {Course} from '../models/course';
import {Technology} from '../models/technology';
import {Period} from '../models/period';
import {InternshipState} from '../enums/internship-state.enum';

@Injectable({
  providedIn: 'root'
})
export class InternshipService {
  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) {
  }

  getAll(): Observable<Internship[]> {
    return this.http.get<Internship[]>(`${this.baseUrl}/api/internship/all`);
  }

  getById(id): Observable<Internship> {
    return this.http.get<Internship>(`${this.baseUrl}/api/internship/${id}`);
  }

  getByState(state): Observable<Internship[]> {
    return this.http.get<Internship[]>(`${this.baseUrl}/api/internship/state/${state}`);
  }

  getInternshipsForStudents(): Observable<Internship[]> {
    return this.http.get<Internship[]>(`${this.baseUrl}/api/internship/student`);
  }

  getStudentFavorites(): Observable<Internship[]> {
    return this.http.get<Internship[]>(`${this.baseUrl}/api/internship/favorite`);
  }

  markAsFavorite(id): Observable<Internship> {
    return this.http.post<Internship>(`${this.baseUrl}/api/internship/${id}/favorite`, id);
  }

  createInternship(internship: Internship): Observable<Internship> {
    return this.http.post<Internship>(`${this.baseUrl}/api/internship`, internship);
  }

  updateInternship(internship: Internship): Observable<Internship> {
    return this.http.put<Internship>(`${this.baseUrl}/api/internship/${internship.id}`, internship);
  }

  updateState(internship: Internship, state: InternshipState, feedback): Observable<Internship> {
    return this.http.put<Internship>(`${this.baseUrl}/api/internship/${internship.id}/state`, {State: state, FeedBack: {value: feedback}});
  }

  updateApprovedState(internship: Internship, state: InternshipState): Observable<Internship> {
    return this.http.put<Internship>(`${this.baseUrl}/api/internship/${internship.id}/state`, {State: state});
  }

  getValidationsById(id): Observable<Validation[]> {
    return this.http.get<Validation[]>(`${this.baseUrl}/api/internship/${id}/validation/all`);
  }

  getValidationsByLector(id): Observable<Validation[]> {
    return this.http.get<Validation[]>(`${this.baseUrl}/api/internship/${id}/validation/lector`);
  }

  // tslint:disable-next-line:ban-types
  updateValidation(id: number, internshipId: number, validation: Object): Observable<Validation> {
    return this.http.put<Validation>(`${this.baseUrl}/api/internship/${internshipId}/validation/${id}`, validation);
  }

  getInternshipForLoggedInLector(): Observable<Internship[]> {
    return this.http.get<Internship[]>(`${this.baseUrl}/api/internship/lector`);
  }

  createValidations(id, ids: number[]): Observable<Validation[]> {
    return this.http.post<Validation[]>(`${this.baseUrl}/api/internship/${id}/validation`, {LectorIds: ids});
  }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.baseUrl}/api/course/all`);
  }

  getTechnologies(): Observable<Technology[]> {
    return this.http.get<Technology[]>(`${this.baseUrl}/api/technology/all`);
  }

  getPeriods(): Observable<Period[]> {
    return this.http.get<Period[]>(`${this.baseUrl}/api/period/all`);
  }

  getInternshipForLoggedInCompany(): Observable<Internship[]> {
    return this.http.get<Internship[]>(`${this.baseUrl}/api/internship/company`);
  }
}
