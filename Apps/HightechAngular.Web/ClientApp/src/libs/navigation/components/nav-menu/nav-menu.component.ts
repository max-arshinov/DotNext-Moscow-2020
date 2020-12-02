import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {Observable} from 'rxjs';
import {MatSidenav} from '@angular/material/sidenav';
import {map, shareReplay} from 'rxjs/operators';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {MenuService} from '../../services/menu.service';
import {menu} from '../../base-menu';
import {IAuthorizeService} from "../../models/iauthorize-service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  @ViewChild('drawer') drawer: MatSidenav;
  public menuItems$: any;
  public isAuthenticated: Observable<boolean>;
  isExpanded = false;

  ngOnInit(): void {
     this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  constructor(private breakpointObserver: BreakpointObserver,
              private menuService: MenuService,
              @Inject('IAuthorizeService') private authorizeService: IAuthorizeService) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
