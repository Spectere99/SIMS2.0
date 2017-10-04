import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { DevExtremeModule } from 'devextreme-angular';

import { AdminRoutingModule } from './admin.routing.module';
import { AdminComponent } from './admin.component';
import { RolesComponent } from './roles/roles.component';
import { UserComponent } from './user/user.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { LookupComponent } from './lookup/lookup.component';

import { PageHeaderModule } from '../shared/'

@NgModule({
    imports: [
        CommonModule,
        NgbDropdownModule.forRoot(),
        AdminRoutingModule,
        TranslateModule,
        DevExtremeModule,
        FormsModule,
        PageHeaderModule
    ],
    declarations: [
        AdminComponent,
        RolesComponent,
        UserComponent,
        PermissionsComponent,
        LookupComponent
    ],
})
export class AdminModule { }
