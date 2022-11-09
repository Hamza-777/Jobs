import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TokenService } from '../services/token-service/token.service';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private http: HttpClient, public tokenservice: TokenService) {}

  ngOnInit(): void {
    this.token_authorize_test();
  }

  token_authorize_test() {
    //Get-JWT Token again
    const token: string = localStorage.getItem('jwt')!;
    console.log(token);
    const tokeninfo: any = jwt_decode(token);
    console.log(tokeninfo);
    this.http.get('https://localhost:7067/WeatherForecast').subscribe({
      next: (result: any) => console.log(result),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  isUserAuthenticated = (): boolean => {
    const token: string = localStorage.getItem('jwt')!;
    if (token) {
      return true;
    } else {
      return false;
    }
  };

  isAdmin = (): boolean => {
    if (this.isUserAuthenticated()) {
      const token: string = localStorage.getItem('jwt')!;
      const tokeninfo: any = jwt_decode(token);
      console.log(tokeninfo);
      if (tokeninfo.Role == 'Admin') {
        return true;
      } else {
        return false;
      }
    }
    return false;
  };

  logOut = () => {
    this.tokenservice.deleteToken();
  };
}
