import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GeneralDashboardComponent} from './containers/general-dashboard/general-dashboard.component';
import {LoginComponent} from './containers/login/login.component';
import {RegisterCompanyComponent} from './containers/register-company/register-company.component';
import {AuthService} from './services/auth.service';


const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: 'home',
    component: GeneralDashboardComponent,
    pathMatch: 'full',
    canActivate: [AuthService]
  },
  {
    path: 'login',
    component: LoginComponent,
    pathMatch: 'full'
  },
  {
    path: 'register',
    component: RegisterCompanyComponent,
    pathMatch: 'full'
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
