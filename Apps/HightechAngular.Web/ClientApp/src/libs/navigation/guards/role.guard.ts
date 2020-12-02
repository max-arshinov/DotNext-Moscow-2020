import {Inject, Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {map} from "rxjs/operators";
import {menu} from '../base-menu';
import {HttpClient} from "@angular/common/http";
import {RoleMenuElement} from "../models/roleMenuElement";
import {IAuthorizeService} from "../models/iauthorize-service";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(@Inject('IAuthorizeService') private authorizeService: IAuthorizeService,
              private http: HttpClient,
              @Inject('rolesApiPath') private rolesApiPath: string,
              private urlMatcher: UrlMatcher,) {
  }

  url: string;

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.http.get<string[]>(`${this.rolesApiPath}`).pipe(
      map(roles => {
        return this.checkAccess(next, state.url, roles);
      })
    );
  }

  checkAccess(route: ActivatedRouteSnapshot, url: string, roles: string[]): boolean {
    this.url = url;
    if (this.notContainsRoles(url))
      return true;

    const menuItem = this.urlMatcher.getMenuItem(url);
    if (!!menuItem) {
      const menuItemRoles = menuItem[1];
      return menuItemRoles.every(x => roles.includes(x))
    }

    return true;
  }

  notContainsRoles(url: string): boolean {
    return [].concat(...menu.map(x=>x.elements))
      .find(x => x.roles == null && url.includes(x.path));
  }
}

@Injectable()
export class UrlMatcher {
  constructor() {}

  get rolesDictionary(): [string, string[]][] {
    return [].concat(...menu.map(x => ((x.elements as RoleMenuElement[]))))
      .map(x => [x.path, x.roles]) as [string, string[]][];
  }

  getMenuItem(url: string): [string, string[]] {
    return this.rolesDictionary.find(([matchUrl, _]) => url == matchUrl);
  }
}
