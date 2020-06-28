import {Person} from './person';
import {Course} from './course';
import {Address} from './address';
import {Internship} from './internship';

export interface Student extends Person {
  course: Course;
  address: Address;
  favorites?: Internship[];
}
