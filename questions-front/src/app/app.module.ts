import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { TestListComponent } from './test-list/test-list.component';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule, JwtModuleOptions } from '@auth0/angular-jwt';
import { TOKEN_KEY } from './services/auth.service';
import { environment } from 'src/environments/environment';
import { SignInComponent } from './sign-in/sign-in.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';

function getToken(): string {
  return localStorage.getItem(TOKEN_KEY)!;
}

const JWT_Module_Options: JwtModuleOptions = {
  config: {
      tokenGetter: getToken,
      allowedDomains: environment.allowedApiDomainsAuth
  }
};

@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    TestListComponent,
    SignInComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    JwtModule.forRoot(JWT_Module_Options),
    NgbModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
