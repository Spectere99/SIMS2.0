import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Http, HttpModule, Headers, RequestMethod, RequestOptions } from '@angular/http';
import { NgModel } from '@angular/forms';
import { NgFor } from '@angular/common';
import { DxDataGridModule,
         DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import CustomStore from 'devextreme/data/custom_store';
import 'rxjs/add/operator/toPromise';
import { UserService, User } from './user.service';
import { RoleService, Role } from '../roles/roles.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [ UserService, RoleService ]
})
export class UserComponent implements OnInit {
  userDataSource: Array<User>;
  roleDataSource: Array<Role>;
  selectedUserRoles = [];
  filteredRoles = [];
  selectedRoles;
  selectedUser;
  showInactive = false;
  popupVisible = false;

  constructor(public userService: UserService, public roleService: RoleService) {
    this.refreshData();
    console.log(this.selectedUser);
   }

   refreshData() {
      this.userService.loadUserData('rwflowers').subscribe(res => this.userDataSource = res);
      this.roleService.loadRoleData('rwflowers').subscribe(res => this.roleDataSource = res);
   }

   updateInactive(d) {
     this.userService.showInactive = this.showInactive;
     this.refreshData();
   }

   createUser(d) {
     let newUser: User = {
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

    let updUser: User = {
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

    this.userService.saveUser(updUser, 'rwflowers').subscribe();
    this.refreshData();
   }

   deactivateUser(d) {
     console.log('Deactivating User');
     console.log(d);

    this.userService.deactivateUser(d.key.id, 'rwflowers').subscribe();
    this.refreshData();
   }

   selectionChanged(data) {
    console.log('Row Clicked');
    // console.log(data.selectedRowsData[0]);
    // console.log(data.selectedRowsData[0].userRoles);
    this.filteredRoles = this.roleDataSource;
    this.selectedUser = data.selectedRowsData[0];
    // console.log(this.selectedUser);
    this.selectedUserRoles = [];
    for (let aRole of data.selectedRowsData[0].userRoles) {
      // console.log(aRole);
      this.selectedUserRoles.push(aRole.role);
      this.filteredRoles = this.filteredRoles.filter(item => item.id !== aRole.role.id);
    }

    // -- Uncomment this code to allow child grid to filter by selected value in other grid (Lookup Type)
    // this.dataGrid.instance.filter(['lookupTypeId', '=', data.selectedRowsData[0].id]);
  }

  moveEditColumnToLeft(e) {
    // console.log('Moving Command Column');
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

  showInfo() {
    console.log('Showing Role Selection');
    console.log(this.roleDataSource);
    this.popupVisible = true;
  }

  ngOnInit(
  ) {
  }

}
