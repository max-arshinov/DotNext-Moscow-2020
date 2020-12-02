import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormBuilderComponent} from './form-builder/form-builder.component';
import {GridBuilderComponent} from './grid-builder/grid-builder.component';
import {MaterialModule} from '../../material.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {FormlyModule} from '@ngx-formly/core';
import {SwaggerSchemaService} from './providers/swagger-schema-service';
import {FormBuilderSchemaService} from './providers/form-builder-schema-service';
import {AgGridModule} from 'ag-grid-angular';
import {GridBuilderSchemaService} from './providers/grid-builder-schema-service';
import { ObjectTypeComponent } from './object.type';
import {
  maxlengthValidationMessage,
  maxValidationMessage,
  minlengthValidationMessage,
  minValidationMessage
} from './form-builder/ValidationMessages';
import {ApiDropdownsService} from './providers/api-dropdowns-service';
import {SetFilterComponent} from '../../components/set-filter/set-filter.component';

@NgModule({
  declarations: [
    FormBuilderComponent,
    GridBuilderComponent,
    ObjectTypeComponent,
    SetFilterComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FormlyModule.forChild(
      {
        validationMessages: [
          {name: 'required', message: 'Это поле обязательно для заполнения'},
          {name: 'minlength', message: minlengthValidationMessage},
          {name: 'maxlength', message: maxlengthValidationMessage},
          {name: 'min', message: minValidationMessage},
          {name: 'max', message: maxValidationMessage},
        ],
        types: [
          {name: 'string', extends: 'input'},
          {
            name: 'number',
            extends: 'input',
            defaultOptions: {
              templateOptions: {
                type: 'number',
              },
            },
          },
          {name: 'boolean', extends: 'checkbox'},
          {name: 'enum', extends: 'select'},
          {name: 'object', component: ObjectTypeComponent},
        ],
      }
    ),
    AgGridModule.withComponents([SetFilterComponent]),
    FormsModule
  ],
  providers: [
    SwaggerSchemaService,
    FormBuilderSchemaService,
    GridBuilderSchemaService,
    ApiDropdownsService
  ],
  exports: [
    FormBuilderComponent,
    GridBuilderComponent
  ]
})
export class BuildersModule {
}
