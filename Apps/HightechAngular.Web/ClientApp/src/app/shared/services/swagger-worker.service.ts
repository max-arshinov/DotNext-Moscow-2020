import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable()
export class SwaggerWorkerService {

  private swaggerUrl = environment.domain + '/swagger/v1/swagger.json';

  constructor(private http: HttpClient) {
  }

  getFormSchema(modelName: string) {
    this.http.get(this.swaggerUrl)
      .pipe(
        map(x => (x as any).components!.schemas![`${modelName}command`].additionalProperties),
        map(x => ({modelApi: x.modelApi, schema: JSON.parse(x.conventionalUI)}))
      );
  }

  getGridSchema(modelName: string) {
    this.http.get(this.swaggerUrl)
      .pipe(
        map(x => (x as any).components!.schemas![`${modelName}query`].additionalProperties),
        map(x => ({modelApi: x.modelApi, schema: JSON.parse(x.conventionalUI)}))
      );
  }
}
