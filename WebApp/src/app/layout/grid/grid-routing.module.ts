import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../shared';

import { GridComponent } from './grid.component';

const routes: Routes = [
    { path: '',
      component: GridComponent,
      canActivate: [AuthGuard],
        data: {modules: [0], loginRedirect: true }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class GridRoutingModule { }
