import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthRouteService implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : boolean {
    if (route.data.authorized) {
      if (this.authService.isAuthenticated()) {
        return true;
      } else {
        this.router.navigate(['auth']);
        return false;
      }
    }
    if (route.data.notUser) {
      if (!this.authService.isAuthenticated()) {
        return true;
      } else {
        this.router.navigate(['']);
        return false;
      }
    }

    return false;
  }
}
