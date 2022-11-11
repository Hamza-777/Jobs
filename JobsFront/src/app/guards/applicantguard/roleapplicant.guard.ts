import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenService } from 'src/app/services/token-service/token.service';

@Injectable({
  providedIn: 'root'
})
export class RoleapplicantGuard implements CanActivate {
  constructor(private tokenservice:TokenService){

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let Role= this.tokenservice.getTokenInfo();
      if(Role['Role']=='Applicant'){
        return true;
      }
    return false;
  }
  
}
