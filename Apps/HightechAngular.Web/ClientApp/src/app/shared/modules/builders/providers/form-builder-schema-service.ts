import {forkJoin, Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {Injectable} from '@angular/core';
import {SwaggerSchemaService} from './swagger-schema-service';
import {FormlyJsonschema} from '@ngx-formly/core/json-schema';
import {ApiDropdownsService} from './api-dropdowns-service';
import {ForceSchemaObject} from '../models/force-schema-object';

@Injectable()
export class FormBuilderSchemaService {
  constructor(private schemaService: SwaggerSchemaService,
              private formlyJsonschema: FormlyJsonschema,
              private dropdownsService: ApiDropdownsService) {
  }

  getFormSchema(type: string): Observable<FormlyFieldConfig[]> {
    return forkJoin([this.schemaService.getSchema(type), this.dropdownsService.getDropdowns(type)]).pipe(
      map(x => this.fillDropdownOptions(x)),
      map(schema => [this.formlyJsonschema.toFieldConfig(schema, {
          map: (x, y) => {
            const property = ((y as any) as ForceSchemaObject);
            if (property.type == 'integer') {
              x.type = 'number';
            }
            if (property.isHidden) {
              x.hide = true;
            }
            if (property.isMultiSelect) {
              x.templateOptions.multiple = true;
            }
            return x;
          }
        })]
      ),
    )
  }

  fillDropdownOptions(result: any) {
    const schema = result[0];
    const dropdownMap = result[1];
    for (const typeName of Object.keys(schema.properties)) {
      const typeInfo = schema.properties[typeName];
      if (typeInfo && typeInfo.isDropdown) {
        const options = dropdownMap[typeName];
        const config = {
          widget: {
            formlyConfig: {
              type: 'enum',
              templateOptions: {
                options
              }
            }
          }
        };
        schema.properties[typeName] = config;
      }
    }
    return schema;
  }
}
