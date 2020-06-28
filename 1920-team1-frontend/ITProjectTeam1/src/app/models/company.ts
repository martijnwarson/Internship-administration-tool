import {Address} from './address';
import {CompanyState} from '../enums/company-state.enum';
import {Person} from './person';
import {Feedback} from './feedback';

export interface Company {
  id: number;
  name: string;
  amountOfEmployees: number;
  amountOfEmployeesIt: number;
  address: Address;
  state: CompanyState;
  contactPerson: Person;
  feedBack?: Feedback;
}
