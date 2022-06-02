import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
