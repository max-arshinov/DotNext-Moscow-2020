import { Component, OnInit } from '@angular/core';
import {BaseFormComponent} from '../../../../shared/modules/builders/form-builder/base-form.component';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';
import {mergeMap} from 'rxjs/operators';
import {iif} from 'rxjs';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss']
})
export class UpdateProductComponent extends BaseFormComponent implements OnInit {
  public model = {};
  formSchemaName = 'FormExample';

  constructor(httpClient: HttpClient, private route: ActivatedRoute, private router: Router) {
    super(httpClient);
  }

  ngOnInit(): void {
    this.route.paramMap
      .pipe(
      mergeMap(result =>
        iif(() => !!result.get('id'),
          this.http.get<any>(`${environment.httpDomain}/api/${this.formSchemaName}/Get/${result.get('id')}`)
        )))
      .subscribe(result => {
        this.model = result;
      });
  }

  modelChange($event: any) {
  }

  submitted(result) {
    this.router.navigateByUrl('/products');
  }
}
