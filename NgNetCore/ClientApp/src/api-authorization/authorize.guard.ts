import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from './authorize.service';
import { tap } from 'rxjs/operators';
import { ApplicationPaths, QueryParameterNames } from './api-authorization.constants';

@Injectable({
    providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {
    constructor(private authorize: AuthorizeService, private router: Router) {
    }
    canActivate(
        _next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return this.authorize.isAuthenticatedRole("Administrador")
            .pipe(tap(isAuthenticated => this.handleAuthorization(isAuthenticated, state)));
    }

    private handleAuthorization(isAuthenticated: boolean, state: RouterStateSnapshot) {
        if (!isAuthenticated) {
            this.router.navigate(ApplicationPaths.LoginPathComponents, {
                queryParams: {
                    [QueryParameterNames.ReturnUrl]: state.url
                }
            });
        }
    }
}

/*
 export class AuthGuard implements CanActivate {
    constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    if (this.authService.isAuthenticated())
    {
      // check if route is restricted by role
      if (route.data.roles ) {

        for (var roleItem of route.data.roles)
        {
          if (this.authService.hasRole(roleItem)) {

            return true;
          }
        }
        //role not authorised so redirect to home page
        this.router.navigate(['/']);
        return false;
      }
      // authorised so return true
      return true;
    }
    // not logged in so redirect to login page with the return url
    //this.authService.login();
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
 * */
