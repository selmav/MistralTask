import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserDetailsComponent } from 'src/user-details/user-details.component';
import { UserListComponent } from 'src/user-list/user-list.component';
import { UserPermissionsComponent } from 'src/user-permissions/user-permissions.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'user-list',
    pathMatch: 'full'
  },
  {
    path: 'user-list',
    component: UserListComponent
  },
  {
    path: 'new-user',
    component: UserDetailsComponent
  },
  {
    path: 'user',
    children: [
      {
        path: ':userId',
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: UserDetailsComponent
          },
          {
            path: 'permissions',
            pathMatch: 'full',
            component: UserPermissionsComponent
          }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
