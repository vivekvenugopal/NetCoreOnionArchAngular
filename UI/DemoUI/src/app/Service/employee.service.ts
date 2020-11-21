import { Injectable } from "@angular/core";
import { HttpClient,HttpErrorResponse,HttpHeaders} from '@angular/common/http'

import {observable, throwError, Observable} from 'rxjs';
import {catchError} from "rxjs/operators";
import { environment } from "../../environments/environment";
import { Employee } from "@model/Employee";

@Injectable({
   providedIn :'root'
})
export class EmployeeService{
    private baseUrl = environment.baseURL+'Employee';
    errors: string[];

    constructor(private _httpClient: HttpClient)
    {
        this.errors = [];

    }
    getEmployees(): Observable<Employee[]>{
       return this._httpClient.get<Employee[]>(this.baseUrl)
       .pipe(catchError(this.handleError));
    }
    getEmployee(id: number): Observable<Employee>{
       
        return this._httpClient.get<Employee>(this.baseUrl+'/'+id)
        .pipe(catchError(this.handleError));
     }
    addEmployee(employee : Employee): Observable<Employee>{
        return this._httpClient.post<Employee>(this.baseUrl, employee, {
            headers:new HttpHeaders({
                'Content-Type':'application/json'
            })
        })
        .pipe(catchError(this.handleError));
    }
    updateEmployee(employee : Employee): Observable<Employee>{
        return this._httpClient.put<Employee>(this.baseUrl, employee, {
            headers:new HttpHeaders({
                'Content-Type':'application/json'
            })
        })
        .pipe(catchError(this.handleError));
    }
    deleteEmployee(id:number): Observable<void>{
        return this._httpClient.delete<void>(this.baseUrl+'/'+id)
        .pipe(catchError(this.handleError));
    }
    private handleError(errorReponse : HttpErrorResponse){
        if (errorReponse.status === 400) {
            // handle validation error
            return throwError(errorReponse.error);
        } else {
            this.errors.push("something went wrong!");
        }
       return throwError(this.errors);
    }
}