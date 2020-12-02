import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {CatalogComponent} from "./catalog.component";

const routes: Routes = [
  {
    path: '',
    component: CatalogComponent,
    // canActivate: [AuthorizeGuard, RoleGuard]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class CatalogRoutingModule { }
