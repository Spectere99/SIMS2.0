import { Component, OnInit, ViewChild, Inject, Output, EventEmitter } from '@angular/core';
import { Http, HttpModule, Headers, RequestMethod, RequestOptions } from '@angular/http';
import { NgModel } from '@angular/forms';
import { DxDataGridModule,
         DxDataGridComponent } from 'devextreme-angular';
import CustomStore from 'devextreme/data/custom_store';
import 'rxjs/add/operator/toPromise';
import { Subscription } from 'rxjs/Subscription';

import { PermissionService, Permission } from '../permissions/permissions.service';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.css'],
  providers: [ PermissionService ]
})
export class PermissionsComponent implements OnInit {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  permissionDataSource: Array<Permission>;
  showInactive = false;

  @Output() onDataChanged = new EventEmitter<any>();

  constructor(public permissionService: PermissionService) {
    this.refreshData();
   }

   refreshData() {
      this.permissionService.loadPermissionData('rwflowers').subscribe(res => this.permissionDataSource = res);
      console.log('Notifying subscribers of update for Data Refresh (From Permissions)');
      this.onDataChanged.emit();
   }

   updateInactive(d) {
     this.permissionService.showInactive = this.showInactive;
     this.refreshData();
   }

   createPermission(d) {
     let newPermission: Permission = {
       id: 0,
       permission: d.data.permission,
       permissionModuleKey: d.data.permissionModuleKey,
       canAccess: d.data.canAccess,
       canUpdate: d.data.canUpdate,
       canDelete: d.data.canDelete,
       isActive: d.data.isActive,
       created: d.data.created,
       createdBy: d.data.createdBy,
       lastUpdated: d.data.lastUpdated,
       lastUpdatebBy: d.data.lastUpdatebBy
     };

     console.log('Creating Permission');
     console.log(newPermission);

     this.permissionService.createPermission(newPermission, 'rwflowers').subscribe(res => this.refreshData());
     this.refreshData();
   }

   savePermission(d) {
    console.log('Saving Permission Change');
    console.log(d);

    let updPermission: Permission = {
      id: d.key.id,
      permission: d.newData.permission === undefined ? d.oldData.permission : d.newData.permission,
      permissionModuleKey: d.newData.permissionModuleKey === undefined ? d.oldData.permissionModuleKey : d.newData.permissionModuleKey,
      canAccess: d.newData.canAccess === undefined ? d.oldData.canAccess : d.newData.canAccess,
      canUpdate: d.newData.canUpdate === undefined ? d.oldData.canUpdate : d.newData.canUpdate,
      canDelete: d.newData.canDelete === undefined ? d.oldData.canDelete : d.newData.canDelete,
      isActive: d.newData.isActive === undefined ? d.oldData.isActive : d.newData.isActive,
      created: d.newData.created === undefined ? d.oldData.created : d.newData.created,
      createdBy: d.newData.createdBy === undefined ? d.oldData.createdBy : d.newData.createdBy,
      lastUpdated: d.newData.lastUpdated === undefined ? d.oldData.lastUpdated : d.newData.lastUpdated,
      lastUpdatebBy: d.newData.lastUpdatebBy === undefined ? d.oldData.lastUpdatebBy : d.newData.lastUpdatebBy
    };

    this.permissionService.savePermission(updPermission, 'rwflowers').subscribe(res => this.refreshData());
    this.refreshData();
   }

   deactivatePermission(d) {
     console.log('Deactivating Permission');
     console.log(d);
    this.permissionService.deactivatePermission(d.key.id, 'rwflowers').subscribe(res => this.refreshData());
    // this.refreshData();
    // this.dataGrid.instance.refresh();
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

