import {Role} from '../enums/role.enum';

export interface Person {
  id?: number;
  name: string;
  firstName: string;
  telephoneNumber: string;
  email: string;
  role: Role;
  title: string;
}
