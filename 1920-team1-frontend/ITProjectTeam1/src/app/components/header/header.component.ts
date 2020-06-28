import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  userName: string;
  constructor(public router: Router, public auth: AuthService) { }

  ngOnInit(): void {
    this.auth.userName.subscribe((name: string) => this.userName = name);
    if (!this.userName) {
      this.userName = localStorage.getItem('userName');
    }
  }

  logout(): void {
    this.auth.logout();
    this.router.navigateByUrl('/login');
  }
}
