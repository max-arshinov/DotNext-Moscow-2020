import {Observable} from 'rxjs';
import {OpenAPIV3} from 'openapi-types';

export interface SchemaService {
  getSchema(type: string): Observable<OpenAPIV3.SchemaObject> ;
}
