import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../shared';

import { AdminComponent } from './admin.component';

const routes: Routes = [
    { path: '',
      component: AdminComponent,
      canActivate: [AuthGuard],
      data: {modules: [0, 1], loginRedirect: false } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
