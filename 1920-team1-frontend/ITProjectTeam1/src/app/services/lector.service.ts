import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Lector} from '../models/lector';
import {Student} from '../models/student';

@Injectable({
  providedIn: 'root'
})
export class LectorService {
  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) {
  }

  getAll(): Observable<Lector[]> {
    return this.http.get<Lector[]>(`${this.baseUrl}/api/lector/all`);
  }

  getById(id): Observable<Lector> {
    return this.http.get<Lector>(`${this.baseUrl}/api/lector/${id}`);
  }

  deleteLector(lectortId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/lector/${lectortId}`);
  }

  createLector(lector: Lector): Observable<Student> {
    return this.http.post<Student>(`${this.baseUrl}/api/lector`, lector);
  }
}
