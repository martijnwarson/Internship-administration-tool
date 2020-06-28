import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Subject} from 'rxjs';
import {CanActivate, CanLoad, Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {
  userName = new Subject();

  constructor(public router: Router, private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) {
  }

  // TODO Add User type && change url to correct one
  login(email: string, password: string) {
    return this.http.post(`${this.baseUrl}/api/login`, { Username: email, Password: password});
  }

  handleLogoutEllipsis(): boolean {
    return localStorage.getItem('loggedIn') === 'true';
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('expires');
    localStorage.removeItem('role');
    localStorage.removeItem('loggedIn');
    localStorage.removeItem('userName');
    this.userName.next('');
  }

  handleAuthentication(token): void {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token);
    const exp = helper.getTokenExpirationDate(token);
    const user = decodedToken.email.split('@', 1);
    this.userName.next(user[0]);

    localStorage.setItem('token', token);
    localStorage.setItem('role', decodedToken.role);
    localStorage.setItem('expires', JSON.stringify(exp));
    localStorage.setItem('loggedIn', 'true');
    localStorage.setItem('userName', user[0]);
  }

  canActivate(): boolean {
    if (localStorage.getItem('loggedIn') !== 'true') {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
