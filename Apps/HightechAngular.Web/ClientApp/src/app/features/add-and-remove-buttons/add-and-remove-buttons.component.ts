import { Component, OnInit } from '@angular/core';
import {ICellRendererAngularComp} from "ag-grid-angular";
import {ICellRendererParams} from "ag-grid-community";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-add-and-remove-buttons',
  templateUrl: './add-and-remove-buttons.component.html',
  styleUrls: ['./add-and-remove-buttons.component.css']
})
export class AddAndRemoveButtonsComponent implements ICellRendererAngularComp {

  constructor(private httpClient: HttpClient) {
  }

  private params;
  agInit(params: ICellRendererParams): void {
    this.params = params;
  }

  refresh(params: any): boolean {
    return true;
  }

  add(){
    this.httpClient.put(environment.domain + this.params.path + '/Add', this.params.data.productId)
      .subscribe(x => this.updateCart());
  }

  remove(){
    this.httpClient.put(environment.domain + this.params.path + '/Remove', this.params.data.productId)
      .subscribe(x => this.updateCart());
  }

  updateCart(){
    this.params.update();
  }
}
