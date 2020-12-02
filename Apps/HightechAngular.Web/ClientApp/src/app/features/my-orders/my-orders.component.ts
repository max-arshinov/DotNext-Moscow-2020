import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {GridColumn} from "../../shared/modules/builders/models/grid-column";
import {IAuthorizeService} from "../../../libs/navigation/models/iauthorize-service";
import {Router} from "@angular/router";
import {MyOrderToNextStateButtonsComponent} from "../my-order-to-next-state-buttons/my-order-to-next-state-buttons.component";
import {GridBuilderComponent} from "../../shared/modules/builders/grid-builder/grid-builder.component";

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {

  additionalColumns: GridColumn[] = [];
  frameWorkComponents: any = {};
  @ViewChild('grid', {static: true}) grid: GridBuilderComponent
  constructor(@Inject('IAuthorizeService') private authorizeService: IAuthorizeService,
              private router: Router) { }

  ngOnInit(): void {
    this.authorizeService.isAuthenticated().subscribe(res => {
      if (!res) {
        this.router.navigate(['/authentication/login']);
      }
    })

    this.additionalColumns.push({
      headerName: '',
      filter: '',
      cellRenderer: "myOrderToNextStateButtonsComponent",
      cellRendererParams:{
        update: this.updatePage.bind(this)
      }
    });

    this.frameWorkComponents = {
      myOrderToNextStateButtonsComponent: MyOrderToNextStateButtonsComponent
    }
  }

  updatePage(){
    this.grid.update();
  }
}
