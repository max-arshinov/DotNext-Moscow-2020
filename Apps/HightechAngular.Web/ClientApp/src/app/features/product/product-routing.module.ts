import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {CreateProductComponent} from './components/create-product/create-product.component';
import {ProductsListComponent} from './components/products-list/products-list.component';
import {UpdateProductComponent} from './components/update-product/update-product.component';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import {RoleGuard} from "../../../libs/navigation/guards/role.guard";

const routes: Routes = [
  {
    path: 'new',
    component: CreateProductComponent,
   // canActivate: [AuthorizeGuard, RoleGuard]
  },
  {
    path: '',
    component: ProductsListComponent,
    //canActivate: [AuthorizeGuard]
  },
  {
    path: 'update/:id',
    component: UpdateProductComponent,
    //canActivate: [AuthorizeGuard]
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
export class ProductRoutingModule { }
