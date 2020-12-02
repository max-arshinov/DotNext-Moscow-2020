import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {BuildersModule} from "../../shared/modules/builders/builders.module";
import {MaterialModule} from "../../shared/material.module";
import {CatalogComponent} from "./catalog.component";
import { ProductInfoComponent } from './product-info/product-info.component'

@NgModule({
  declarations: [
    //CatalogComponent,
    ProductInfoComponent],
  imports: [
    CommonModule,
    BuildersModule,
    MaterialModule
  ]
})
export class CatalogModule {
}
