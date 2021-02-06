import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {ShowCompleteMessageService} from "../services/show-complete-message-service"
import {catchError} from "rxjs/operators"

@Injectable()
export class ServerSideMessageInterceptor implements HttpInterceptor {

  constructor(private showCompleteMessageService: ShowCompleteMessageService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError(err => {
          if (err.status == 422 || err.status == 500) {
            let message = err.error.title;
            if(!message){
              message = 'Internal Server Error';
            }

            this.showCompleteMessageService.show(message);
          }

          return throwError(err);
        })
      );
  }
}
