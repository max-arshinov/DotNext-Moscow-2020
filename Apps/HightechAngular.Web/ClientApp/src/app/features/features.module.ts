import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FeaturesRoutingModule} from './features-routing.module';
import {MaterialModule} from '../shared/material.module';
import {FormBuilderDemoComponent} from './form-builder-demo/form-builder-demo.component';
import {GridBuilderDemoComponent} from './gird-builder-demo/grid-builder-demo.component';
import {BuildersModule} from '../shared/modules/builders/builders.module';
import {ApiDropdownsService} from '../shared/modules/builders/providers/api-dropdowns-service';
import {ProductModule} from './product/product.module';
import {CatalogModule} from "./catalog/catalog.module";
import { MainPageComponent } from './main-page/main-page.component';
import {BestsellersComponent} from "./main-page/bestsellers/bestsellers.component";
import {NewArrivalsComponent} from "./main-page/new-arrivals/new-arrivals.component";
import { SaleComponent } from './main-page/sale/sale.component';
import { AddToCartButtonComponent } from './add-to-cart-button/add-to-cart-button.component';
import { CartComponent } from './cart/cart.component';
import { AddAndRemoveButtonsComponent } from './add-and-remove-buttons/add-and-remove-buttons.component';
import { CreateOrderComponent } from './create-order/create-order.component';
import { AdminComponent } from './admin/admin.component';
import {DeleteProductButtonComponent} from "./delete-product-button/delete-product-button.component";
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { MyOrderToNextStateButtonsComponent } from './my-order-to-next-state-buttons/my-order-to-next-state-buttons.component';
import { AdminOrderToNextStateButtonsComponent } from './admin-order-to-next-state-buttons/admin-order-to-next-state-buttons.component';


@NgModule({
  declarations: [
    FormBuilderDemoComponent,
    GridBuilderDemoComponent,
    BestsellersComponent,
    MainPageComponent,
    NewArrivalsComponent,
    SaleComponent,
    AddToCartButtonComponent,
    CartComponent,
    AddAndRemoveButtonsComponent,
    CreateOrderComponent,
    AdminComponent,
    DeleteProductButtonComponent,
    MyOrdersComponent,
    MyOrderToNextStateButtonsComponent,
    AdminOrderToNextStateButtonsComponent,

  ],
    imports: [
        CommonModule,
        FeaturesRoutingModule,
        MaterialModule,
        BuildersModule,
        ProductModule,
        CatalogModule,
    ],
  exports: [
    BestsellersComponent
  ],
  providers: [
    {provide: 'DropdownsService', useClass: ApiDropdownsService}
  ]
})
export class FeaturesModule {
}
