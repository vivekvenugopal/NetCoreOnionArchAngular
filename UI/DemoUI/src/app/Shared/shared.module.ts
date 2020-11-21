import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AngularMaterialsModule } from './angular-material.module';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ConfirmationDialogComponent } from './PopupDialogs/ConfirmDialog/ConfirmationDialog.component';
import { AlertDialogComponent } from './PopupDialogs/alert-dialog/alert-dialog.component';
import { LoadingOverlayComponent } from './Controls/loading-overlay/loading-overlay.component';

@NgModule({
  declarations: [
    ConfirmationDialogComponent,
    AlertDialogComponent, LoadingOverlayComponent],
    imports: [
    AngularMaterialsModule,
    CommonModule
    ],
  exports: [
    CommonModule,
    AngularMaterialsModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    RouterModule,
    HttpClientModule,
    LoadingOverlayComponent],
    entryComponents: [
      ConfirmationDialogComponent,
      AlertDialogComponent
    ],
})
export class SharedModule { }
