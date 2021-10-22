import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthRouteService } from './services/auth-route.service';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { TestListComponent } from './test-list/test-list.component';

const routes: Routes = [
  {
    path: '',
    component: TestListComponent,
    canActivate: [ AuthRouteService ],
    data: {
      authorized: true
    }
  },
  {
    path: 'auth',
    data: {
      notUser: true
    },
    canActivate: [ AuthRouteService ],
    children: [
      {
        path: '',
        redirectTo: 'sign-in',
        pathMatch: 'full'
      },
      {
        path: 'sign-up',
        component: SignUpComponent
      },
      {
        path: 'sign-in',
        component: SignInComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
