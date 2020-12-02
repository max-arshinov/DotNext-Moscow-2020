import {DropdownsService} from './dropdowns-service';
import {DropdownInfo} from '../models/dropdown-info';
import {Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable()
export class ApiDropdownsService implements DropdownsService {
  constructor(private http: HttpClient) {
  }

  getDropdowns(type: string, params = null): Observable<Map<string, DropdownInfo>> {
    return this.http.get(`${environment.httpDomain}/Dropdowns/Get/${type}`, {params: params}) as Observable<Map<string, DropdownInfo>>
  }
}
