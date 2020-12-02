import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {mergeMap} from 'rxjs/operators';
import {MenuHeader} from '../models/menuHeader';
import {RoleMenuElement} from '../models/roleMenuElement';

@Injectable()
export class MenuService {
  constructor(@Inject('menuToken') private menuApiPath: string,
              private http: HttpClient) {
  }

  getMenuByRoles(headers: MenuHeader[]): Observable<any[]> {
    const menuElements = [].concat(...headers.map(header => header.elements)) as RoleMenuElement[];
    const allRoles = Array.from(new Set([].concat(...menuElements.map(x => x.roles)))).map(role => `role=${role}&`);
    return this.http.get<string[]>(`${this.menuApiPath}?${allRoles}`)
      .pipe(
        mergeMap(roles => {
          const filteredElements = menuElements.filter(element =>
            element.roles.some(role => roles.includes(role)) ||
            !element.roles.length);
          const result = [].concat(headers.map(header => ({
            name: header.name,
            elements: header.elements.filter(element => filteredElements.includes(element as RoleMenuElement))
          })));
          return of(result);
        })
      );
  }
}
