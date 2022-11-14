import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenService } from 'src/app/services/token-service/token.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RolerecruiterGuard implements CanActivate {
  constructor(private tokenservice:TokenService, private router: Router){

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let Role= this.tokenservice.getTokenInfo();
      if(Role['Role']=='Recruiter'){
        return true;
      } else {
        this.router.navigateByUrl("/page-not-found");
        return false;
      }
  }
  
}
