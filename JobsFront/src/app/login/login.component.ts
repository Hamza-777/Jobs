import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import {Router} from '@angular/router'
import { NgForm } from '@angular/forms';
import { LoginModel } from '../_interfaces/login.model';
import { AuthenticatedResponse } from '../_interfaces/authenticatedresponse.model';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth-service/auth.service';
import { TokenService } from '../services/token-service/token.service';
import { apiresponse } from '../_interfaces/apiresponse';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;
  credentials: LoginModel = {userdata:'', password:''};
  error:any;
  constructor(private router: Router,private http:HttpClient,private auth:AuthService,private tokenservice:TokenService, private handlerservice:GlobalerrorhandlerService) {
   }
  ngOnInit(): void {
    
  }
  login = ( form: NgForm) => {
    if (form.valid) {
      this.auth.loginuser(this.credentials)
      .subscribe({
        next: (response: apiresponse) => {
          if (response.message == "") {
            this.error = this.handlerservice.handleError(response.error);
          } else {
            this.tokenservice.addToken(response.data.token);
            this.invalidLogin = false; 
            this.router.navigate(["/"]);
            console.log(response.data.token);
          }
          
        },
        error: (err: HttpErrorResponse) => this.invalidLogin = true
      })
    }
  }
}
