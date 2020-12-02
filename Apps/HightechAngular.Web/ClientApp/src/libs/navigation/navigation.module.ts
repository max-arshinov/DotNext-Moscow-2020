import {NgModule} from '@angular/core';
import {NavMenuComponent} from "./components/nav-menu/nav-menu.component";
import {SideNavComponent} from "./components/nav-menu/side-nav.component";
import {HttpClientModule} from "@angular/common/http";
import {MaterialModule} from "../../app/shared/material.module";
import {UrlMatcher} from "./guards/role.guard";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {LoginMenuComponent} from "./components/login-menu/login-menu.component";
import {MenuService} from "./services/menu.service";

@NgModule({
  declarations: [
    NavMenuComponent,
    SideNavComponent,
    LoginMenuComponent
  ],
  imports: [
    HttpClientModule,
    MaterialModule,
    CommonModule,
    RouterModule,
  ],
  providers: [
    UrlMatcher,
    MenuService
  ],
  exports: [
    NavMenuComponent,
  ]
})
export class NavigationModule {
}
