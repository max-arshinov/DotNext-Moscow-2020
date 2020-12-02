import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {AppComponent} from './app.component';
import {HomeComponent} from './home/home.component';
import {ApiAuthorizationModule} from 'src/api-authorization/api-authorization.module';
import {FormlyMaterialModule} from '@ngx-formly/material';
import {FormlyModule} from '@ngx-formly/core';
import {AuthorizeInterceptor} from 'src/api-authorization/authorize.interceptor';
import {SharedModule} from './shared/shared.module';
import {MaterialModule} from './shared/material.module';
import {NavigationModule} from "../libs/navigation/navigation.module";
import {AuthorizeService} from "../api-authorization/authorize.service";
import {CatalogComponent} from "./features/catalog/catalog.component";
import {BuildersModule} from "./shared/modules/builders/builders.module";
import {MainPageComponent} from "./features/main-page/main-page.component";
import {CartComponent} from "./features/cart/cart.component";
import {AdminComponent} from "./features/admin/admin.component";
import {ShowCompleteMessageService} from "./shared/services/show-complete-message-service";
import {MyOrdersComponent} from "./features/my-orders/my-orders.component";
import {ServerSideMessageInterceptor} from "./shared/interceptors/server-side-message.interceptor"

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CatalogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    SharedModule,
    FormlyMaterialModule,
    MaterialModule,
    BrowserAnimationsModule,
    BuildersModule,
    FormlyModule.forRoot(),
    RouterModule.forRoot([
      {path: '', component: MainPageComponent, pathMatch: 'full'},
      {path: 'catalog', component: CatalogComponent},
      {path: 'cart', component: CartComponent},
      {path: 'myOrders', component: MyOrdersComponent},
      {path: 'admin', component: AdminComponent},
      {
        path: 'products',
        loadChildren: () => import('./features/features.module').then(x => x.FeaturesModule),
      },
    ]),
    NavigationModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ServerSideMessageInterceptor, multi: true },
    { provide: 'menuToken', useValue: '/api/menu/menuByRoles' },
    { provide: 'rolesApiPath', useValue: '/api/menu/getUserRoles' },
    { provide: 'IAuthorizeService', useClass: AuthorizeService },
    ShowCompleteMessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
