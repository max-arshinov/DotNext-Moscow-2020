import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {FormGroup} from "@angular/forms";

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})

export class ProductInfoComponent {
  public form = new FormGroup({});
  constructor( @Inject(MAT_DIALOG_DATA) public model: any) {
  }
}
