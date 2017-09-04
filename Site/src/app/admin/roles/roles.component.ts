import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Http, HttpModule, Headers, RequestMethod, RequestOptions } from '@angular/http';
import { NgModel } from '@angular/forms';
import { DxDataGridModule,
         DxDataGridComponent } from 'devextreme-angular';
import CustomStore from 'devextreme/data/custom_store';
import 'rxjs/add/operator/toPromise';
import { RoleService, Role } from './roles.service';
import { PermissionService, Permission } from '../permissions/permissions.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css'],
  providers: [ RoleService, PermissionService ]
})
export class RolesComponent implements OnInit {
  roleDataSource: Array<Role>;
  permissionDataSource: Array<Permission>;
  showInactive = false;

  constructor(public roleService: RoleService, public permissionService: PermissionService) {
    this.refreshData();
   }

   refreshData() {
      this.roleService.loadRoleData('rwflowers').subscribe(res => this.roleDataSource = res);
      this.permissionService.loadPermissionData('rwflowers').subscribe(res => this.permissionDataSource = res);
   }

   updateInactive(d) {
     this.roleService.showInactive = this.showInactive;
     this.refreshData();
   }

   createRole(d) {
     let newRole: Role = {
       id: 0,
       role: d.data.role,
       permissionId: d.data.permissionId,
       isActive: d.data.isActive,
       created: d.data.created,
       createdBy: d.data.createdBy,
       lastUpdated: d.data.lastUpdated,
       lastUpdatebBy: d.data.lastUpdatebBy
     };

     console.log('Creating Role');
     console.log(newRole);

     this.roleService.createRole(newRole, 'rwflowers').subscribe();
     this.refreshData();
   }

   saveRole(d) {
    console.log('Saving User Change');
    console.log(d);

    let updRole: Role = {
      id: d.key.id,
      role: d.newData.role === undefined ? d.oldData.role : d.newData.role,
      permissionId: d.newData.permissionId === undefined ? d.oldData.permissionId : d.newData.permissionId,
      isActive: d.newData.isActive === undefined ? d.oldData.isActive : d.newData.isActive,
      created: d.newData.created === undefined ? d.oldData.created : d.newData.created,
      createdBy: d.newData.createdBy === undefined ? d.oldData.createdBy : d.newData.createdBy,
      lastUpdated: d.newData.lastUpdated === undefined ? d.oldData.lastUpdated : d.newData.lastUpdated,
      lastUpdatebBy: d.newData.lastUpdatebBy === undefined ? d.oldData.lastUpdatebBy : d.newData.lastUpdatebBy
    };

    this.roleService.saveRole(updRole, 'rwflowers').subscribe();
    this.refreshData();
   }

   deactivateRole(d) {
     console.log('Deactivating Role');
     console.log(d);

    this.roleService.deactivateRole(d.key.id, 'rwflowers').subscribe();
    this.refreshData();
   }

   selectionChanged(data) {
    // console.log(data.selectedRowsData[0].type);
    // console.log(this.dataGrid);
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
  ngOnInit() {
  }

}
