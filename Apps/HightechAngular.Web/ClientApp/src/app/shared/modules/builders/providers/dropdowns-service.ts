import {DropdownInfo} from '../models/dropdown-info';
import {Observable} from 'rxjs';

export interface DropdownsService {
  getDropdowns(type: string, params): Observable<Map<string, DropdownInfo>>;
}
