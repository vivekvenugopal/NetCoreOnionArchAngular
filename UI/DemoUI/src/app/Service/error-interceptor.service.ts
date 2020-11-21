import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from './authentication.service'
import { ErrorMessage } from '@model/ErrorMessage';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            let errorMessage = new ErrorMessage();
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.authenticationService.logout();
                location.reload(true);
            }
            else if (err.status === 400) {
                return throwError(err);
            }
            else if(err.status === 500){
                errorMessage.ErrorCode = 500;
                errorMessage.Description = err.error.Description;
                errorMessage.Message = err.error.Message;
                return throwError(errorMessage);
            }
            else if(err.status === 422)
                errorMessage = this.Error422(err);
            else if(!err.error.Message)
                err.error.Message = 'Unable to connect server. Please contact your administrator';
            return throwError(errorMessage);
        }))
    }
    Error422(err:any):ErrorMessage{
        let errorMessage = new ErrorMessage();
        errorMessage.ErrorCode = 422;
        errorMessage.FieldName = err.error.FieldName;
        errorMessage.Message = err.error.Message;
        return errorMessage;
    }
}