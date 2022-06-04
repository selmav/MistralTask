import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserDetailsComponent } from 'src/user-details/user-details.component';
import { UserListComponent } from 'src/user-list/user-list.component';

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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
