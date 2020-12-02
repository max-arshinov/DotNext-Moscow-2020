import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {GridColumn} from "../../shared/modules/builders/models/grid-column";
import {GridBuilderComponent} from "../../shared/modules/builders/grid-builder/grid-builder.component";
import {UpdateProductComponent} from "../product/components/update-product/update-product.component";
import {CreateProductComponent} from "../product/components/create-product/create-product.component";
import {DeleteProductButtonComponent} from "../delete-product-button/delete-product-button.component";
import {IAuthorizeService} from "../../../libs/navigation/models/iauthorize-service";
import {Router} from "@angular/router";
import {AdminOrderToNextStateButtonsComponent} from "../admin-order-to-next-state-buttons/admin-order-to-next-state-buttons.component";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  categoriesApiRoute: Record<string, string>;
  categoriesSchemaNames: Record<string, string>;
  additionalColumnsDict: Record<string, GridColumn[]>
  additionalFrameWorkComponentsDict: Record<string, any>
  categories: string[] = [];
  selectedCategory: string;
  basePath: string = '';

  additionalColumns: GridColumn[] = [];
  frameWorkComponents: any = {}

  @ViewChild('grid', {static: true}) grid: GridBuilderComponent

  productAdditionalColumns: GridColumn[] = [];
  productFrameWorkComponents: any = {};

  orderAdditionalColumns: GridColumn[] = [];
  orderFrameWorkComponents: any = {};

  constructor(private http: HttpClient,
              private dialog: MatDialog,
              @Inject('IAuthorizeService') private authorizeService: IAuthorizeService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.authorizeService.isAuthenticated().subscribe(res => {
      if (!res) {
        this.router.navigate(['/authentication/login']);
      }
    })

    this.basePath = "/api";

    this.categoriesApiRoute = {
      "Orders": "/Order",
    };

    this.categoriesSchemaNames = {
      "Orders": "OrderListItem",
    };


    this.initAdditionalProductComponents();
    this.initOrderAdditionalComponents();


    this.additionalColumnsDict = {
      "Orders": this.orderAdditionalColumns,
      "Products": this.productAdditionalColumns
    }

    this.additionalFrameWorkComponentsDict = {
      "Orders": this.orderFrameWorkComponents,
      "Products": this.productFrameWorkComponents
    }

    this.categories.push("Orders"); //"Products"

    this.selectedCategory = this.categories[0];
  }

  changeCategory(newCategory: string) {
    this.selectedCategory = newCategory;
    this.updateGrid();
  }

  updateProduct(row) {
    this.dialog.open(UpdateProductComponent, {
      data: row.dataPath
    })
  }

  createProduct(): void {
    this.dialog.open(CreateProductComponent, {})
  }

  updateGrid() {
    this.grid.schemaName = this.categoriesSchemaNames[this.selectedCategory];
    this.grid.path = this.basePath + this.categoriesApiRoute[this.selectedCategory];
    this.grid.additionalColumns = this.additionalColumnsDict[this.selectedCategory];
    this.grid.additionalFrameWorkComponents = this.additionalFrameWorkComponentsDict[this.selectedCategory];
    this.grid.reloadSchema();
    this.grid.update();
  }

  initAdditionalProductComponents() {
    this.productAdditionalColumns.push({
        headerName: '',
        filter: '',
        cellRenderer: 'deleteProductButtonComponent',
        sortable: false,
        cellRendererParams: {
          path: '/ProductManagement'
        }
      },
      {
        headerName: '',
        filter: '',
        cellRenderer: 'updateProductComponent',
        sortable:false,
        cellRendererParams: {
          path: '/ProductManagement'
        }
      });

    this.productFrameWorkComponents = {
      deleteProductButtonComponent: DeleteProductButtonComponent,
      updateProductComponent: UpdateProductComponent
    }
  }

  initOrderAdditionalComponents() {
      this.orderAdditionalColumns.push({
        headerName: '',
        filter: '',
        cellRenderer: 'adminOrderToNextStateButtonsComponent',
        sortable: false,
        cellRendererParams: {
          update: this.updatePage.bind(this)
        }
      })

    this.orderFrameWorkComponents = {
      adminOrderToNextStateButtonsComponent: AdminOrderToNextStateButtonsComponent
    }
  }

  updatePage(){
    this.grid.update();
  }
}
