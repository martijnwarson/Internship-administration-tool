import {Component, OnInit} from '@angular/core';
import {LectorService} from '../../services/lector.service';
import {Lector} from '../../models/lector';

@Component({
  selector: 'app-general-dashboard',
  templateUrl: './general-dashboard.component.html',
  styleUrls: ['./general-dashboard.component.scss']
})
export class GeneralDashboardComponent implements OnInit {
  role: string;
  showApplications: boolean;
  showInternshipsApplications: boolean;
  showCompanyApplications: boolean;
  showInternships: boolean;
  showStudents: boolean;
  showLectors: boolean;
  showCompanies: boolean;
  showStudentInternships: boolean;

  constructor(private ls: LectorService) {
  }

  ngOnInit(): void {
    this.role = localStorage.getItem('role');
    this.checkRole(this.role);
  }

  checkRole(role): void {
    switch (role) {
      case 'COÖRDINATOR': {
        this.showApplications = false;
        this.showInternshipsApplications = false;
        this.showCompanyApplications = true;
        this.showInternships = true;
        this.showStudents = true;
        this.showLectors = true;
        this.showCompanies = true;
        this.showStudentInternships = false;
        break;
      }
      case 'LECTOR': {
        this.showApplications = false;
        this.showInternshipsApplications = false;
        this.showCompanyApplications = false;
        this.showInternships = true;
        this.showStudents = false;
        this.showLectors = false;
        this.showCompanies = false;
        this.showStudentInternships = false;
        break;
      }
      case 'STUDENT': {
        this.showApplications = false;
        this.showInternshipsApplications = false;
        this.showCompanyApplications = false;
        this.showInternships = false;
        this.showStudents = false;
        this.showLectors = false;
        this.showCompanies = false;
        this.showStudentInternships = true;
        break;
      }
      case 'CONTACT': {
        this.showApplications = true;
        this.showInternshipsApplications = true;
        this.showCompanyApplications = false;
        this.showInternships = false;
        this.showStudents = false;
        this.showLectors = false;
        this.showCompanies = false;
        this.showStudentInternships = false;
        break;
      }
      default: {
        this.showApplications = false;
        this.showInternshipsApplications = false;
        this.showCompanyApplications = false;
        this.showInternships = false;
        this.showStudents = false;
        this.showLectors = false;
        this.showCompanies = false;
        this.showStudentInternships = false;
      }
    }
  }
}
