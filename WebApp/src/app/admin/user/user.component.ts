import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Http, HttpModule, Headers, RequestMethod, RequestOptions } from '@angular/http';
import { NgModel } from '@angular/forms';
import { NgFor } from '@angular/common';
import { DxDataGridModule,
         DxDataGridComponent } from 'devextreme-angular';
import notify from 'devextreme/ui/notify';

import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import CustomStore from 'devextreme/data/custom_store';
import 'rxjs/add/operator/toPromise';
import { UserService, User } from './user.service';
import { RoleService, Role } from '../roles/roles.service';
import { UserRoleService, UserRole } from '../_services/userrole.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [ UserService, RoleService, UserRoleService ]
})
export class UserComponent implements OnInit {
  userDataSource: Array<User>;
  roleDataSource: Array<Role>;
  selectedUserRoles = [];
  filteredRoles = [];
  selectedRoles;
  selectedUser;
  rolesChanged = false;
  showInactive = false;
  popupVisible = false;

  phonePattern: any = /^\(\d{3}\)\\d{3}-\d{4}$/i;

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  constructor(public userService: UserService, public roleService: RoleService, public userRoleService: UserRoleService) {
    this.refreshData();
    // console.log(this.selectedUser);
    console.log('Refreshing Data');
   }

   refreshData(deselect?: boolean) {
    console.log('Refresh Data of Users');
      this.userService.loadUserData('rwflowers').subscribe(res => this.userDataSource = res);
      this.roleService.loadRoleData('rwflowers').subscribe(res => this.roleDataSource = res);
      /* if (deselect) {
          this.dataGrid.instance.deselectAll();
          this.selectedUserRoles = [];
          this.filteredRoles = [];
      } */
   }

   updateInactive(d) {
     this.userService.showInactive = this.showInactive;
     this.refreshData();
   }

   createUser(d) {
     const newUser: User = {
       id: 0,
       userName: d.data.userName,
       email: d.data.email,
       firstName: d.data.firstName,
       lastName: d.data.lastName,
       contactNumber: d.data.contactNumber,
       isActive: d.data.isActive,
       created: d.data.created,
       createdBy: d.data.createdBy,
       lastUpdated: d.data.lastUpdated,
       lastUpdatebBy: d.data.lastUpdatebBy
     };

     console.log('Creating User');
     console.log(newUser);

     this.userService.createUser(newUser, 'rwflowers').subscribe();
     this.refreshData();
   }

   saveUser(d) {
    console.log('Saving User Change');
    console.log(d);

    const updUser: User = {
      id: d.key.id,
      userName: d.newData.userName === undefined ? d.oldData.userName : d.newData.userName,
      email: d.newData.email === undefined ? d.oldData.email : d.newData.email,
      firstName: d.newData.firstName === undefined ? d.oldData.firstName : d.newData.firstName,
      lastName: d.newData.lastName === undefined ? d.oldData.lastName : d.newData.lastName,
      contactNumber: d.newData.contactNumber === undefined ? d.oldData.contactNumber : d.newData.contactNumber,
      isActive: d.newData.isActive === undefined ? d.oldData.isActive : d.newData.isActive,
      created: d.newData.created === undefined ? d.oldData.created : d.newData.created,
      createdBy: d.newData.createdBy === undefined ? d.oldData.createdBy : d.newData.createdBy,
      lastUpdated: d.newData.lastUpdated === undefined ? d.oldData.lastUpdated : d.newData.lastUpdated,
      lastUpdatebBy: d.newData.lastUpdatebBy === undefined ? d.oldData.lastUpdatebBy : d.newData.lastUpdatebBy
    };

    this.userService.saveUser(updUser, 'rwflowers').subscribe(res => this.refreshData());
   }

   deactivateUser(d) {
    console.log('Deactivating User');
    console.log(d);

    this.userService.deactivateUser(d.key.id, 'rwflowers').subscribe(res => this.refreshData());
   }

   addRole(Id: number) {
    console.log('Adding role');
          for (let i = 0; i < this.filteredRoles.length; i++) {
            if (this.filteredRoles[i].id === Id) {
              this.selectedUserRoles.push(this.filteredRoles[i]);
              this.filteredRoles.splice(i, 1);
              this.rolesChanged = true;
            }
          }
          // console.log(this.selectedUserRoles);
          // console.log(this.filteredRoles);
          this.sortSelectedUserRoles();
   }

   updateUserRole(id: number, userId: string) {
    console.log('Updating User Roles for userId:' + id);
    console.log(this.selectedUser);
    console.log('Selected Roles to Save');
    console.log(this.selectedUserRoles);

    for (const existingRole of this.selectedUser.userRoles) {
      console.log('Removing...');
      console.log(existingRole);
      this.userRoleService.deleteUserRole(existingRole.id, userId).subscribe();
    }
    for (const selRole of this.selectedUserRoles){
      console.log('Adding...');
      console.log(selRole);
      const newUserRole: UserRole = {
        id: 0,
        userId: id,
        roleId: selRole.id,
        isActive: true,
        created: new Date().toLocaleDateString(),
        createdBy: userId,
        lastUpdated: new Date().toLocaleDateString(),
        lastUpdatedBy: userId
      };
      console.log('newUserRole...');
      console.log(newUserRole);

      this.userRoleService.createUserRole(newUserRole, userId).subscribe(res => this.refreshData(true));
     }
     this.userService.loadUserData('rwflowers').subscribe(res => this.userDataSource = res);
     notify('User Role saved for user: ' + this.selectedUser.userName, 'success', 2000);
   }

   removeRole(Id: number) {
      for (let i = 0; i < this.selectedUserRoles.length; i++) {
        if (this.selectedUserRoles[i].id === Id) {
          this.filteredRoles.push(this.selectedUserRoles[i]);
          this.selectedUserRoles.splice(i, 1);
          this.rolesChanged = true;
        }
      }

      this.filteredRoles.sort((left, right): number => {
        if (left.role < right.role) { return -1; }
        if (left.role > right.role) { return 1; }
        return 0;
      });
   }

   selectionChanged(data) {
    console.log('Row Clicked');
    this.filteredRoles = this.roleDataSource;
    this.selectedUser = data.selectedRowsData[0];
    this.selectedUserRoles = [];
    if (data.selectedRowsData[0] !== undefined) {
      for (const aRole of data.selectedRowsData[0].userRoles) {
        this.selectedUserRoles.push(aRole.role);
        this.filteredRoles = this.filteredRoles.filter(item => item.id !== aRole.roleId);
      }
      this.sortFilteredRoles();
    }
  }

  moveEditColumnToLeft(e) {
    e.component.columnOption('command:edit',
    {
      visibleIndex: -1,
      width: 80
    });
  }

  formatCommandColumn(e) {
    if (e.rowType === 'data' && e.column.command === 'edit') {
      const isEditing = e.row.isEditing;
      const links = e.cellElement.find('.dx-link');

      links.text('');

      if (isEditing) {
        links.filter('.dx-link-save').addClass('dx-icon-save');
        links.filter('.dx-link-cancel').addClass('dx-icon-revert');
      } else {
        links.filter('.dx-link-edit').addClass('dx-icon-edit');
        links.filter('.dx-link-delete').addClass('dx-icon-trash');
      }

    }
  }

  sortFilteredRoles() {
    this.filteredRoles.sort((left, right): number => {
      if (left.role < right.role) { return -1; }
      if (left.role > right.role) { return 1; }
      return 0;
    });
  }

  sortSelectedUserRoles() {
    this.selectedUserRoles.sort((left, right): number => {
      if (left.role < right.role) { return -1; }
      if (left.role > right.role) { return 1; }
      return 0;
    });
  }
  showInfo() {
    console.log('Showing Role Selection');
    console.log(this.roleDataSource);
    this.popupVisible = true;
  }

  ngOnInit(
  ) {
  }

}
