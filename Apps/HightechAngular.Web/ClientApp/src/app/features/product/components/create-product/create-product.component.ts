import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BaseFormComponent} from '../../../../shared/modules/builders/form-builder/base-form.component';
import {Router} from '@angular/router';
import {environment} from "../../../../../environments/environment";
import {MAT_DIALOG_DATA, MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent extends BaseFormComponent implements OnInit {
  constructor(http: HttpClient,
              private router: Router, private dialog: MatDialog) {
    super(http);
  }

  ngOnInit(): void {
  }

  public

  submitted(result) {
    this.router.navigateByUrl('/products');
  }

  modelChange($event: any) {
  }

  create(data: any) {
    this.http.post(environment.domain + "/ProductManagement/Create", this.form.value)
      .subscribe(result => {
        this.dialog.closeAll();
        this.router.navigate(['/admin'])
      });
  }
}
