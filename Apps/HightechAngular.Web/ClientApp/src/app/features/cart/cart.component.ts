import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {GridColumn} from "../../shared/modules/builders/models/grid-column";
import {AddAndRemoveButtonsComponent} from "../add-and-remove-buttons/add-and-remove-buttons.component";
import {GridBuilderComponent} from "../../shared/modules/builders/grid-builder/grid-builder.component";
import {MatDialog} from "@angular/material/dialog";
import {CreateOrderComponent} from "../create-order/create-order.component";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {ShowCompleteMessageService} from "../../shared/services/show-complete-message-service";
import {IAuthorizeService} from "../../../libs/navigation/models/iauthorize-service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  constructor(private httpClient: HttpClient,
              private dialog: MatDialog,
              private messageService: ShowCompleteMessageService,
              @Inject('IAuthorizeService') private authorizeService: IAuthorizeService,
              private router: Router) {
  }

  additionalColumns: GridColumn[] = [];
  frameWorkComponents: any = {};
  sumPrice = 0;
  isAuthenticated;

  @ViewChild('grid', {static: true}) grid: GridBuilderComponent;

  ngOnInit(): void {
    this.authorizeService.isAuthenticated().subscribe(res => this.isAuthenticated = res);
    this.additionalColumns.push({
      headerName: '',
      filter: '',
      cellRenderer: 'addAndRemoveToCartButtonsConponent',
      cellRendererParams: {
        path: '/Cart',
        update: this.updateCart.bind(this)
      }
    })

    this.frameWorkComponents = {
      addAndRemoveToCartButtonsConponent: AddAndRemoveButtonsComponent
    }

    this.grid.isLoaded.subscribe(x => {
      if (this.grid.rowData.length) {
        let sumPrice = 0;
        this.grid.rowData.forEach(x => sumPrice += x.count * x.price);
        this.sumPrice = sumPrice;
      }
    })
  }

  updateCart() {
    this.grid.update()
  }

  private get isCartEmpty() {
    return !(this.grid.rowData.length > 0);
  }


  order() {
    const headerDict = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Allow-Headers': 'Content-Type',
    }

    const requestOptions = {
      headers: new HttpHeaders(headerDict)
    };
    if (!this.isAuthenticated) {
      this.router.navigate(['/authentication/login']);
    } else {
      this.httpClient
        .post(environment.domain + "/MyOrders/CreateNew", requestOptions)
        .subscribe(res => {
          this.messageService.show('Order created');
          this.dialog.open(CreateOrderComponent, {
            data: {
              products: this.grid.rowData,
              total: this.sumPrice,
              orderId: res
            },
            minWidth: 400
          })
        })
    }
  }
}
