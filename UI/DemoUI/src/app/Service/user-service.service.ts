import { Injectable } from "@angular/core";
import { HttpClient,HttpErrorResponse,HttpHeaders, HttpParams} from '@angular/common/http'

import {observable, throwError, Observable} from 'rxjs';
import {catchError} from "rxjs/operators";
import { environment } from "../../environments/environment";
import { UserRegistration } from "@model/UserRegistration";

@Injectable({
   providedIn :'root'
})
export class UserService{
    private baseUrl = environment.baseURL+'User/';
    errors: string[];

    constructor(private _httpClient: HttpClient)
    {
        this.errors = [];

    }
    getUserRegistrations(): Observable<UserRegistration[]>{
       return this._httpClient.get<UserRegistration[]>(this.baseUrl+'UserRegistrations');
    }
    activateUser(id:number): Observable<void>{
     
        return this._httpClient.post<void>(this.baseUrl+'ActivateUser', id, {
            headers:new HttpHeaders({
                'Content-Type':'application/json;'
            })
        });
    }
    deactivateUser(id:number): Observable<void>{
        return this._httpClient.post<void>(this.baseUrl+'DeActivateUser', id, {
            headers:new HttpHeaders({
                'Content-Type':'application/json;'
            })
        });
    }
}