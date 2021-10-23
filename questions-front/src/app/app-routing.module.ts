import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthRouteService } from './services/auth-route.service';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { TestAnswerComponent } from './test-answer/test-answer.component';
import { TestListComponent } from './test-list/test-list.component';

const routes: Routes = [
  {
    path: '',
    data: {
      authorized: true
    },
    canActivate: [ AuthRouteService ],
    children: [
      {
        path: '',
        component: TestListComponent
      },
      {
        path: 'test-answer',
        component: TestAnswerComponent
      }
    ]
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
