import {Lector} from './lector';
import {ValidationState} from '../enums/validation-state.enum';
import {Feedback} from './feedback';

export interface Validation {
  id: number;
  lector: Lector;
  state: ValidationState;
  date: string | Date;
  feedBack?: Feedback;
}
