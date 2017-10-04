import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Http, HttpModule, Headers, RequestMethod, RequestOptions } from '@angular/http';
import { NgModel } from '@angular/forms';
import { DxDataGridModule,
         DxDataGridComponent } from 'devextreme-angular';
import CustomStore from 'devextreme/data/custom_store';
import 'rxjs/add/operator/toPromise';
import { LookupService, Lookup, LookupType } from './lookup.service';

@Component({
  selector: 'app-lookup',
  templateUrl: './lookup.component.html',
  styleUrls: ['./lookup.component.css'],
  providers: [ LookupService ]
})
export class LookupComponent implements OnInit {
  @ViewChild('lookupGrid') dataGrid: DxDataGridComponent;
  lookupDataSource: Array<Lookup>;
  lookupTypeDataSource: Array<LookupType>;
  showInactive = false;

  constructor(public lookupService: LookupService) {
    // this.lookupsDataSource = this.lookupService.loadLookupData('rwflowers');
    lookupService.loadLookupType('rwflowers').subscribe(res => this.lookupTypeDataSource = res);
    lookupService.loadLookupData('rwflowers').subscribe(res => this.lookupDataSource = res);
  }

  refreshData() {
    this.lookupService.loadLookupType('rwflowers').subscribe(res => this.lookupTypeDataSource = res);
    this.lookupService.loadLookupData('rwflowers').subscribe(res => this.lookupDataSource = res);
  }

  updateInactive(d) {
    console.log(this.showInactive);
    this.lookupService.showInactive = this.showInactive;
    this.refreshData();
  }
  createLookup(d) {
    console.log(d.data);

    let newLookup: Lookup = {
      id: 0,
      value: d.data.value,
      lookupTypeId: d.data.lookupTypeId,
      lookupType: null,
      isActive: true
    };

    console.log('Creating Lookup');
    console.log(newLookup);

    this.lookupService.createLookupValue(newLookup, 'rwflowers')
    .subscribe();
  }

  createLookupType(d) {
    console.log(d.data);

    let newLookupType: LookupType = {
      id: 0,
      typeDescription: d.data.typeDescription,
      isActive: true
    };

    console.log(newLookupType);

    this.lookupService.createLookupTypeValue(newLookupType , 'rwflowers')
      .subscribe();

    this.refreshData();
  }

  saveLookupType(d) {
    console.log('Saving Lookup Type Change');
    console.log(d);

    let updatedLookupType: LookupType = {
      id: d.key.id,
      typeDescription: d.newData.typeDescription === undefined ? d.oldData.typeDescription : d.newData.typeDescription,
      isActive: d.newData.isActive === undefined ? d.oldData.isActive : d.newData.isActive
    };

    this.lookupService.saveLookupTypeValues(updatedLookupType, 'rwflowers')
    .subscribe();
  }

  saveLookup(d) {
    console.log('Saving Lookup Change');
    console.log(d);

    let updatedLookup: Lookup = {
      id: d.key.id,
      value: d.newData.value === undefined ? d.oldData.value : d.newData.value,
      lookupTypeId: d.newData.lookupTypeId === undefined ? d.oldData.lookupTypeId : d.newData.lookupTypeId,
      lookupType: null,
      isActive: d.newData.isActive === undefined ? d.oldData.isActive : d.newData.isActive
    };

    this.lookupService.saveLookupValues(updatedLookup, 'rwflowers')
    .subscribe();
  }

  deactivateLookupType(d) {
    console.log('Deactivating LookupType');
    console.log(d);
    this.lookupService.deactivateLookupType(d.key.id, 'rwflowers').subscribe();
    this.refreshData();
  }

  deactivateLookup(d) {
    console.log('Deactivating Lookup');
    console.log(d);
    this.lookupService.deactivateLookup(d.key.id, 'rwflowers').subscribe();
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
