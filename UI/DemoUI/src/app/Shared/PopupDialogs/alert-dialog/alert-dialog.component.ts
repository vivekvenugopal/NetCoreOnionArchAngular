import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-confirm-component',
  templateUrl: './alert-dialog.component.html'
})
export class AlertDialogComponent implements OnInit {
  ngOnInit(): void {
   
  }
  constructor(
    public dialogRef: MatDialogRef<AlertDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public message: string) { }
    
}
