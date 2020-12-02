import {Component, OnInit} from '@angular/core';
import {ICellRendererAngularComp} from "ag-grid-angular";
import {ICellRendererParams} from "ag-grid-community";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-my-order-to-next-state-buttons',
  templateUrl: './my-order-to-next-state-buttons.component.html',
  styleUrls: ['./my-order-to-next-state-buttons.component.css']
})
export class MyOrderToNextStateButtonsComponent implements ICellRendererAngularComp {
  private params: any;
  isNoAnyActionForState: boolean;
  private controllerName: string;
  currentState: string;

  constructor(private httpClient: HttpClient) {
  }

  agInit(params: ICellRendererParams): void {
    this.params = params;
    this.isNoAnyActionForState = params.data.status !== "Shipped"
      && params.data.status !== "New";
    this.controllerName = "/MyOrders";
    this.currentState = params.data.status;
  }

  refresh(params: any): boolean {
    return false;
  }

  dispute() {
    this.httpClient.put(environment.domain + this.controllerName + "/Dispute", {orderId: this.params.data.id})
      .subscribe(x => this.params.update());
  }


  complete() {
    this.httpClient.put(environment.domain + this.controllerName + "/Complete", {orderId: this.params.data.id})
      .subscribe(x => this.params.update());
  }

  paid() {
    this.httpClient.put(environment.domain + this.controllerName + "/PayOrder", {orderId: this.params.data.id})
      .subscribe(x => this.params.update());
  }


}
