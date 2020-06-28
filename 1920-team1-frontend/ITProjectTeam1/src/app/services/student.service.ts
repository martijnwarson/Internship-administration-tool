import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Student} from '../models/student';
import {Internship} from '../models/internship';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) {
  }

  getAll(): Observable<Student[]> {
    return this.http.get<Student[]>(`${this.baseUrl}/api/student/all`);
  }

  getById(id): Observable<Student> {
    return this.http.get<Student>(`${this.baseUrl}/api/student/${id}`);
  }

  createStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(`${this.baseUrl}/api/student`, student);
  }

  deleteStudent(studentId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/student/${studentId}`);
  }

  uploadCsv(file): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    const headers = new HttpHeaders({ enctype: 'multipart/form-data' });
    return this.http.post<any>(`${this.baseUrl}/api/student/upload`, formData, {headers});
  }
}
