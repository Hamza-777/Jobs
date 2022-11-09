import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import {Router} from '@angular/router'
import { NgForm } from '@angular/forms';
import { LoginModel } from '../_interfaces/login.model';
import { AuthenticatedResponse } from '../_interfaces/authenticatedresponse.model';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth-service/auth.service';
import { TokenService } from '../services/token-service/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin!: boolean;
  credentials: LoginModel = {userdata:'', password:''};
  constructor(private router: Router,private http:HttpClient,private auth:AuthService,private tokenservice:TokenService) {
   }
  ngOnInit(): void {
    
  }
  login = ( form: NgForm) => {
    if (form.valid) {
      this.auth.loginuser(this.credentials)
      .subscribe({
        next: (response: AuthenticatedResponse) => {
          this.tokenservice.addToken(response);
          this.invalidLogin = false; 
          this.router.navigate(["/"]);
        },
        error: (err: HttpErrorResponse) => this.invalidLogin = true
      })
    }
  }
}
