import {Course} from './course';
import {Technology} from './technology';
import {Person} from './person';
import {Address} from './address';
import {Student} from './student';
import {Validation} from './validation';
import {Period} from './period';
import {InternshipState} from '../enums/internship-state.enum';
import {Feedback} from './feedback';

export interface Internship {
  // TO BE CHECKED
  id?: number;
  courses?: Course[];
  technologies?: Technology[];
  students?: Student[];
  validations?: Validation[];
  periods?: Period[];
  state?: InternshipState;


  technologyIds: number[];
  courseIds: number[];
  periodIds: number[];
  studentIds: number[];
  title: string;
  description: string;
  contactPerson: Person;
  promotors: Person[];
  address: Address;
  techDescription: string;
  researchTopic: string;
  application: boolean;
  resumee: boolean;
  reimbursement: boolean;
  studentAmount: number;
  remarks: string;
  nrOfSupportEmployees: number;
  conditions?: string;
  companyId?: string;
  favorite?: boolean;
  feedBack?: Feedback;
}
