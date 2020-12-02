import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProductsListComponent} from './components/products-list/products-list.component';
import {CreateProductComponent} from './components/create-product/create-product.component';
import {UpdateProductComponent} from './components/update-product/update-product.component';
import {BuildersModule} from '../../shared/modules/builders/builders.module';
import {ProductRoutingModule} from './product-routing.module';
import {MaterialModule} from '../../shared/material.module';
import { DeleteProductComponent } from './components/delete-product/delete-product.component';


@NgModule({
  declarations: [ProductsListComponent, CreateProductComponent, UpdateProductComponent, DeleteProductComponent],
  imports: [
    CommonModule,
    BuildersModule,
    ProductRoutingModule,
    MaterialModule
  ]
})
export class ProductModule {
}
