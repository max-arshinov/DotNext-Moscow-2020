import {Component, OnInit, ViewChild} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {MatDialog} from "@angular/material/dialog";
import {ProductInfoComponent} from "./product-info/product-info.component";
import {GridBuilderComponent} from "../../shared/modules/builders/grid-builder/grid-builder.component";
import {GridColumn} from "../../shared/modules/builders/models/grid-column";
import {AddToCartButtonComponent} from "../add-to-cart-button/add-to-cart-button.component";
import {ShowCompleteMessageService} from "../../shared/services/show-complete-message-service";

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {

  constructor(private httpClient: HttpClient, private dialog: MatDialog, private messageService: ShowCompleteMessageService) {
  }
  categories: any;
  selectedCategory = 1;
  additionalColumns: GridColumn[] = [];
  frameWorkComponents: any = {};
  @ViewChild('grid', {static:false}) grid: GridBuilderComponent;

  ngOnInit(): void {
    this.httpClient.get(environment.domain + "/Catalog/GetCategories")
      .subscribe(data =>{
        this.categories = data;
      })
    this.additionalColumns.push({
      headerName: '',
      filter: '',
      cellRenderer: 'addToCartButtonConponent',
      cellRendererParams: {
        path: '/Cart/Add',
        httpMethod: 'put',
        add: this.addToCart.bind(this),
      }
    })

    this.frameWorkComponents = {
      addToCartButtonConponent: AddToCartButtonComponent
    }
  }

  addToCart(rowId) {
    this.httpClient.put(environment.domain + '/Cart/Add', rowId).subscribe(x => {
      this.messageService.show('Successfully added to your cart')
    })
  }

  clickRow(row){
    this.dialog.open(ProductInfoComponent, {
      data: row.data
    })
  }

  changeCategory(data){
    this.selectedCategory = data;
    this.grid.filterParams = {};
    this.grid.filterParams['categoryId'] = data;
    this.grid.update();
    this.grid.reloadSchema(this.grid.filterParams);
  }
}
