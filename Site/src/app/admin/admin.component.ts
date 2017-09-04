import { Component, OnInit, ViewChild } from '@angular/core';
import { DevExtremeModule } from 'devextreme-angular';

import { RolesComponent } from './roles/roles.component';
import { UserComponent } from './user/user.component';
import { PermissionsComponent } from './permissions/permissions.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  @ViewChild(RolesComponent)
  private roleComponent: RolesComponent;

  @ViewChild(UserComponent)
  private userComponent: UserComponent;

  @ViewChild(PermissionsComponent)
  private permissionComponent: PermissionsComponent;

  constructor() { }
  ngOnInit() { }

  notifyPermissionsUpdate() {
    this.roleComponent.refreshData();
    this.userComponent.refreshData(true);
  }

  notifyRolesUpdate() {
    this.userComponent.refreshData(true);
  }
}
