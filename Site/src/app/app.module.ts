import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Http, HttpModule, Headers, RequestMethod, RequestOptions } from '@angular/http';
import { FormsModule, NgModel } from '@angular/forms';

import { AppComponent } from './app.component';
import { AdminComponent } from './admin/admin.component';
import { LookupComponent } from './admin/lookup/lookup.component';

import { DevExtremeModule } from 'devextreme-angular';
import { UserComponent } from './admin/user/user.component';
import { RolesComponent } from './admin/roles/roles.component';
import { PermissionsComponent } from './admin/permissions/permissions.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    LookupComponent,
    UserComponent,
    RolesComponent,
    PermissionsComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    DevExtremeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
