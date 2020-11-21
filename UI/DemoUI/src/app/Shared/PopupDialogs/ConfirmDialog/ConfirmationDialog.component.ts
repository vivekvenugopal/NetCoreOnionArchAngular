import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-confirm-component',
  templateUrl: './ConfirmationDialog.component.html'
})
export class ConfirmationDialogComponent implements OnInit {
  ngOnInit(): void {
   
  }
  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public message: string) {
      
     }
    
    onConfirm(): void {
      // Close the dialog, return true
      this.dialogRef.close(true);
    }
   
    onDismiss(): void {
      // Close the dialog, return false
      this.dialogRef.close(false);
    }
}
