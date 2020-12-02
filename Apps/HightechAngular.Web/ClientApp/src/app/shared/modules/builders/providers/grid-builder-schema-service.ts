import {forkJoin, Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Injectable} from '@angular/core';
import {SwaggerSchemaService} from './swagger-schema-service';
import {ForceSchemaObject} from '../models/force-schema-object';
import {ApiDropdownsService} from './api-dropdowns-service';
import {OpenAPIV3} from 'openapi-types';
import {DropdownInfo} from '../models/dropdown-info';
import {GridColumn} from '../models/grid-column';

@Injectable()
export class GridBuilderSchemaService {
  constructor(private schemaService: SwaggerSchemaService, private dropdownsService: ApiDropdownsService  ) {
  }

  buildColumns(schema: ForceSchemaObject, parent?: string, dropdownOptions?: Map<string, DropdownInfo>) {
    const properties = Object.entries(schema.properties || {});
    const findKey = (key, options) => {
      return Object.keys(options).find(x => x.toLowerCase() === key.toLowerCase());
    };
    return properties.map(y => {
      const [key, property] = y;
      const fieldPath = parent ? `${parent.toLowerCase()}.${key}` : key;
      const column: GridColumn = {
        headerName: property.title,
        field: fieldPath,
        sortable: true,
        hide: property.isHidden,
        filter: 'setFilter',
        menuTabs: ['filterMenuTab'],
        filterParams: {
          options: dropdownOptions? dropdownOptions[findKey(fieldPath, dropdownOptions)] : []
        }
      };
      if (property.$ref) {
        const refPath = property.$ref.split('/').slice(-1).pop();
        const ref = schema.components.schemas[refPath];
        if (ref.properties) {
          column.children = this.buildColumns(ref, fieldPath, dropdownOptions);
        }
      }
      return column;
    });
  }

  getGridSchema(type: string, params = null): Observable<GridColumn[]> {
    return forkJoin([
      this.schemaService.getSchema(type),
      this.dropdownsService.getDropdowns(type, params)])
    .pipe(
      map(x => {
        const schema = x[0];
        const dropdowns = x[1];
        return this.buildColumns(schema, null, dropdowns);
      })
    );
  }
}
