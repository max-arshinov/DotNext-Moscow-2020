import { Component, OnInit } from '@angular/core';
import {ICellRendererAngularComp} from "ag-grid-angular";
import {ICellRendererParams} from "ag-grid-community";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";


@Component({
  selector: 'app-delete-product-button',
  templateUrl: './delete-product-button.component.html',
  styleUrls: ['./delete-product-button.component.css']
})
export class DeleteProductButtonComponent implements ICellRendererAngularComp {

  private params;
  agInit(params: ICellRendererParams): void {
    this.params = params;
  }

  refresh(params: any): boolean {
    return true;
  }

  constructor(private httpClient: HttpClient) { }

  delete() {
    this.httpClient.delete(environment.domain + this.params.path, this.params.data.id)
      .subscribe();
  }
}
