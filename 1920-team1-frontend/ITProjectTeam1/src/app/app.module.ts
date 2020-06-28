import {BrowserModule} from '@angular/platform-browser';
import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {GeneralDashboardComponent} from './containers/general-dashboard/general-dashboard.component';
import {HeaderComponent} from './components/header/header.component';
import {SharedModule} from './shared/shared.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {LoginComponent} from './containers/login/login.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {InternshipsComponent} from './containers/internships/internships.component';
import {ItemListComponent} from './components/item-list/item-list.component';
import {AuthInterceptor} from './shared/auth.interceptor';
import {BaseInterceptor} from './shared/base.interceptor';
import {environment} from '../environments/environment';
import { AssignLectorComponent } from './components/assign-lector/assign-lector.component';
import {MatDialogModule} from '@angular/material/dialog';
import { RegisterCompanyComponent } from './containers/register-company/register-company.component';
import { InternshipFormComponent } from './components/internship-form/internship-form.component';
import { CreateInternshipComponent } from './containers/create-internship/create-internship.component';
import { NewCompaniesComponent } from './containers/new-companies/new-companies.component';
import { NewCompanyListComponent } from './components/company-list/new-company-list.component';
import { CompaniesComponent } from './containers/companies/companies.component';
import { CompanyInternshipsComponent } from './containers/company-internships/company-internships.component';
import { TextInputDialogComponent } from './components/text-input-dialog/text-input-dialog.component';
import { LectorsComponent } from './containers/lectors/lectors.component';
import { StudentsComponent } from './containers/students/students.component';
import { LectorListComponent } from './components/lector-list/lector-list.component';
import { StudentListComponent } from './components/student-list/student-list.component';
import {NgxSpinnerModule} from 'ngx-spinner';
import { StudentInternshipListComponent } from './components/student-internship-list/student-internship-list.component';
import { StudentInternshipsComponent } from './containers/student-internships/student-internships.component';
import { NewStudentDialogComponent } from './components/student-list/new-student-dialog/new-student-dialog.component';
import { FileUploadDialogComponent } from './components/file-upload-dialog/file-upload-dialog.component';



@NgModule({
  declarations: [
    AppComponent,
    GeneralDashboardComponent,
    HeaderComponent,
    LoginComponent,
    HeaderComponent,
    InternshipsComponent,
    ItemListComponent,
    AssignLectorComponent,
    RegisterCompanyComponent,
    InternshipFormComponent,
    CreateInternshipComponent,
    NewCompaniesComponent,
    NewCompanyListComponent,
    CompaniesComponent,
    CompanyInternshipsComponent,
    TextInputDialogComponent,
    LectorsComponent,
    StudentsComponent,
    LectorListComponent,
    StudentListComponent,
    StudentInternshipListComponent,
    StudentInternshipsComponent,
    NewStudentDialogComponent,
    FileUploadDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatDialogModule,
    NgxSpinnerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    { provide: 'BASE_API_URL', useValue: environment.apiUrl }

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent],
  entryComponents: [AssignLectorComponent, TextInputDialogComponent, NewStudentDialogComponent, FileUploadDialogComponent]
})
export class AppModule {
}
