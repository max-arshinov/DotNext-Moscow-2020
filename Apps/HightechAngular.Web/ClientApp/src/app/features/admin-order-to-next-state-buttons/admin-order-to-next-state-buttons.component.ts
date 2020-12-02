import { Component} from '@angular/core';
import {ICellRendererAngularComp} from "ag-grid-angular";
import {ICellRendererParams} from "ag-grid-community";
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-admin-order-to-next-state-buttons',
  templateUrl: './admin-order-to-next-state-buttons.component.html',
  styleUrls: ['./admin-order-to-next-state-buttons.component.css']
})
export class AdminOrderToNextStateButtonsComponent implements ICellRendererAngularComp {
  private params: any;
  private controllerName: string;
  currentState: string;
  nextStatesDict: Record<string, string>
  changeStageFuncDict: Record<string, Function>
  noActionMessage: string;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

  agInit(params: ICellRendererParams): void {
    this.params = params;
    this.currentState = this.params.data.status;
    this.noActionMessage = "No action";
    this.nextStatesDict = {
      "New" : "Paid",
      "Paid" : "Shipped",
      "Dispute": "Complete",
      "Shipped":this.noActionMessage,
      "Complete":this.noActionMessage
    };

    this.changeStageFuncDict = {
      "New" : this.paid.bind(this),
      "Dispute": this.complete.bind(this),
      "Paid":this.shipped.bind(this),
      "Shipped":this.noActionStub,
      "Complete": this.noActionStub
    }

    this.controllerName = "/Order";
  }

  refresh(params: any): boolean {
    return false;
  }

  paid() {
    this.httpClient.put(environment.domain + this.controllerName + "/PayOrder", {orderId: this.params.data.id})
      .subscribe(x => this.params.update());
  }

  shipped() {
    this.httpClient.put(environment.domain + this.controllerName + "/Shipped", {orderId: this.params.data.id})
      .subscribe(x => this.params.update());
  }

  complete() {
    this.httpClient.put(environment.domain + this.controllerName + "/Complete", {orderId: this.params.data.id})
      .subscribe(x => this.params.update());
  }

  noActionStub(){

  }
}
