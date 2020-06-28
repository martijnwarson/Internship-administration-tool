import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Validation} from '../models/validation';
import {Company} from '../models/company';
import {Internship} from '../models/internship';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) {
  }

  createCompany(company: Company): Observable<Company> {
    return this.http.post<Company>(`${this.baseUrl}/api/company`, company);
  }

  getAll(): Observable<Company[]> {
    return this.http.get<Company[]>(`${this.baseUrl}/api/company/all`);
  }

  getById(id): Observable<Company> {
    return this.http.get<Company>(`${this.baseUrl}/api/company/${id}`);
  }

  getByState(state): Observable<Company> {
    return this.http.get<Company>(`${this.baseUrl}/api/company/state/${state}`);
  }

  updateCompanyState(companyId: number, state: number, feedback): Observable<Internship> {
    return this.http.put<Internship>(`${this.baseUrl}/api/company/${companyId}/state`, {companystate: state, feedBack: {value: feedback}});
  }
  updateAcceptedCompanyState(companyId: number, state: number): Observable<Internship> {
    return this.http.put<Internship>(`${this.baseUrl}/api/company/${companyId}/state`, {companystate: state});
  }
}
