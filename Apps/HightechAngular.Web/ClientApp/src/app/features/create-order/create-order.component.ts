import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog} from "@angular/material/dialog";
import {FormGroup} from "@angular/forms";
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GridColumn} from "../../shared/modules/builders/models/grid-column";
import {SetFilterComponent} from "../../shared/components/set-filter/set-filter.component";
import {Router} from "@angular/router";
import {ShowCompleteMessageService} from "../../shared/services/show-complete-message-service";

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {

  public form = new FormGroup({});
  model: any;

  constructor(@Inject(MAT_DIALOG_DATA)
              public data: any, private httpClient: HttpClient,
              private dialog: MatDialog,
              private router: Router,
              private messageService: ShowCompleteMessageService) { }

  ngOnInit(): void {
  }

  order(){
    this.httpClient.put(environment.domain + "/Order/PayOrder", {orderId: this.data.orderId})
      .subscribe(x => {
        this.dialog.closeAll();
        this.messageService.show('Order paid')
        this.router.navigate(['/catalog']);
      });
  }

}

