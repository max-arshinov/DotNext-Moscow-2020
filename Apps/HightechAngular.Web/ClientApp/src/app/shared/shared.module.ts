import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';
import {FormlyModule} from '@ngx-formly/core';
import {SwaggerWorkerService} from './services/swagger-worker.service';
import {MaterialModule} from './material.module';
import {RouterModule} from '@angular/router';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormlyModule.forChild(),
    MaterialModule,
    RouterModule
  ],
  providers: [SwaggerWorkerService],
  exports: []
})
export class SharedModule {
}
