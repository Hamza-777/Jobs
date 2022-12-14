import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  addToken(response: any) {
    const token = response;
    localStorage.setItem('jwt', token);
  }

  deleteToken() {
    localStorage.removeItem('jwt');
  }

  getToken() {
    return localStorage.getItem('jwt')!;
  }

  isUserAuthenticated = (): boolean => {
    const token: string = this.getToken();
    if (token) {
      return true;
    } else {
      return false;
    }
  };

  getTokenInfo() {
    if (this.isUserAuthenticated()) {
      const tokeninfo: any = jwt_decode(this.getToken());
      console.log(tokeninfo);
      return tokeninfo;
    }
    return false;
  }

  isAdmin = (): boolean => {
    if (this.isUserAuthenticated()) {
      const token: string = this.getToken();
      const tokeninfo: any = jwt_decode(token);
      if (tokeninfo.Role == 'Admin') {
        return true;
      } else {
        return false;
      }
    }
    return false;
  };

  isRecruiter = (): boolean => {
    if (this.isUserAuthenticated()) {
      const token: string = this.getToken();
      const tokeninfo: any = jwt_decode(token);
      if (tokeninfo.Role == 'Recruiter') {
        return true;
      } else {
        return false;
      }
    }
    return false;
  };

  isApplicant = (): boolean => {
    if (this.isUserAuthenticated()) {
      const token: string = this.getToken();
      const tokeninfo: any = jwt_decode(token);
      if (tokeninfo.Role == 'Applicant') {
        return true;
      } else {
        return false;
      }
    }
    return false;
  };
}
