import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        console.log('IsLoggedIn:');
        console.log(localStorage.getItem('isLoggedin'));
        console.log(route.data['modules']);
        // console.log(state);

        const allowedModules = route.data['modules'] as Array<number>;
        console.log('Allowed Modules');
        console.log(allowedModules);
        if (allowedModules === undefined || allowedModules.indexOf(1) !== -1) {
            if (localStorage.getItem('isLoggedin')) {
                return true;
            }
        } else {
            alert('Not allowed');
        }

        if (route.data['loginRedirect'] !== undefined || route.data['loginRedirect'] === true){
            this.router.navigate(['/login']);
        }
        return false;
    }
}
