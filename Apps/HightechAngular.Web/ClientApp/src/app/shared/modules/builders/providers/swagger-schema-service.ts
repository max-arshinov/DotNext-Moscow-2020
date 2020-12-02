import {SchemaService} from './schema-service';
import {Observable, Subject} from 'rxjs';
import {map, publishReplay, refCount, shareReplay, tap} from 'rxjs/operators';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../../environments/environment';
import {OpenAPIV3} from 'openapi-types'
import {Injectable} from '@angular/core';
import {ForceSchemaObject} from '../models/force-schema-object';

@Injectable()
export class SwaggerSchemaService implements SchemaService {
  private swaggerUrl = environment.swaggerUrl
  private schema$: Observable<ForceSchemaObject>;

  constructor(private http: HttpClient) {
  }

  getSchema(type: string): Observable<ForceSchemaObject> {
      this.schema$ = this.http
        .get(this.swaggerUrl)
        .pipe(
          map(x => this.resolveRefs((x as OpenAPIV3.Document), type)),
          map(x => x.components!.schemas![type] as ForceSchemaObject),
          publishReplay(1),
          refCount());

    return this.schema$;
  }

  resolveRefs(schema: OpenAPIV3.Document, type: string) {
    const schemaObject = schema.components.schemas[type] as any;
    if (schemaObject.properties) {
      for (const item of Object.values(schemaObject.properties) as any) {
        if (item.$ref) {
          const ref = item.$ref;
          const typeInfo = this.getTypeInfo(ref, schema);
          if (!schemaObject.components) {
            schemaObject.components = {
              schemas: {}
            };
          }
          if (!typeInfo.definition.enum) {
            typeInfo.definition.isDropdown = true;
          }
          schemaObject.components.schemas[typeInfo.name] = typeInfo.definition;
          if (typeInfo.definition.properties) {
            this.resolveRefs(schema, typeInfo.name);
          }
        }
      }
    }
    return schema;
  }

  getTypeInfo(ref: string, schema: OpenAPIV3.Document) {
    const pointer = ref.substr(2);
    const paths = !pointer ? null : pointer.split('/');
    const name = paths[paths.length - 1];
    const definition = !paths ? null : paths.reduce(
      (
        (def, path) => def && def.hasOwnProperty(path) ? def[path] : null
      ),
      schema
    );
    return {
      name,
      definition
    };
  }
}
