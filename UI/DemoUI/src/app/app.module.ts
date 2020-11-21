import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { PageModule } from './Pages/page.module';
import {   BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtInterceptor } from '@service/jwt-interceptor.service';
import { ErrorInterceptor } from '@service/error-interceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AngularMaterialsModule } from '@shared/angular-material.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    PageModule,
    AngularMaterialsModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  exports: [AppRoutingModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
