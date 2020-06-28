import {Person} from './person';
import {Course} from './course';

export interface Lector extends Person {
  courses: Course[];
}
