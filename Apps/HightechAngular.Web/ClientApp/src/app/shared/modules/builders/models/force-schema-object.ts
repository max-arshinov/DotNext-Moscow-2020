import {OpenAPIV3} from 'openapi-types';

export interface ForceSchemaObject extends OpenAPIV3.NonArraySchemaObject {
  components: {
    schemas: {}
  };
  title: string;
  isHidden: boolean;
  isMultiSelect: boolean;
  properties?: {
    [name: string]: ForceSchemaObject;
  };
  allOf?: Array<OpenAPIV3.ReferenceObject>;
  $ref?: string;
}
