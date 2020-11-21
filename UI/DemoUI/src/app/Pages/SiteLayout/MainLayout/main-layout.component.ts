import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Route } from '@angular/compiler/src/core';
import { ConfirmationDialogComponent } from '@shared/PopupDialogs/ConfirmDialog/ConfirmationDialog.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-Main-Layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent implements OnInit {

  constructor(private _authenticationService: AuthenticationService, private router: Router,
    private dialog: MatDialog,) { }
  activatecontrol : boolean;
  ngOnInit() {
    if(this._authenticationService.currentUserValue)
       this.activatecontrol = true
    else
       this.activatecontrol=false;
  }

  Logout(){
    this._authenticationService.logout();
    this.router.navigate(['Login']);
  }
  logoutPrompt(){
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: "Are you sure you want to logout?"
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.Logout();
      }
      else {
        dialogRef.close();
      }
    });
  }
}
