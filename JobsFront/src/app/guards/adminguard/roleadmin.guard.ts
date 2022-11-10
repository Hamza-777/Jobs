import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenService } from '../../services/token-service/token.service';

@Injectable({
  providedIn: 'root'
})
export class RoleadminGuard implements CanActivate {
  constructor(private tokenservice:TokenService,private router:Router){

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      
      let Role= this.tokenservice.getTokenInfo();
      if(Role['Role']=='Admin'){
        return true;
      }
      this.router.navigateByUrl('/courses')
    return false;
  }
  
}
