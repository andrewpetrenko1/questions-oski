import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Token } from '../interfaces/token';
import { User } from '../interfaces/user';
import { map, tap } from 'rxjs/operators';

export const TOKEN_KEY = 'access_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + "/auth";
  public user: User = {id: null, login: '', email: ''};

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService
  ) { 
    if(this.user.id === null)
      this.decodeToken();
  }

  decodeToken() {
    try {
      let tokenInfo = this.jwtHelper.decodeToken(localStorage.getItem(TOKEN_KEY) as string);
      this.user.id = Number(tokenInfo.sub);
      this.user.login = tokenInfo.unique_name;
      this.user.email = tokenInfo.email;
    } catch {
      this.router.navigate(['auth']);
    }
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem(TOKEN_KEY);
    return token !== null && !this.jwtHelper.isTokenExpired(token);
  }

  public signIn(user: User): Observable<Token> {
    return this.httpClient.post<Token>(`${this.apiUrl}/sign-in`, user)
    .pipe(
      tap(info => {
        this.initStorageInfo(info);
        this.decodeToken();
      })
    );
  }

  public signUp(user: User): Observable<Token> {
    return this.httpClient.post<Token>(`${this.apiUrl}/sign-up`, user)
    .pipe(
      tap(info => {
        this.initStorageInfo(info);
        this.decodeToken();
      })
    );
  } 

  private initStorageInfo(token: Token) {
    localStorage.setItem(TOKEN_KEY, token.access_token);
  }



}
