import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { async, waitForAsync } from '@angular/core/testing';
import { Router } from '@angular/router';
import { asyncScheduler } from 'rxjs';
import { RegisterModel } from '../_interfaces/register.model';
import { RegisterResponse } from '../_interfaces/registerresponse.model';

@Component({
  selector: 'app-forgotpwd',
  templateUrl: './forgotpwd.component.html',
  styleUrls: ['./forgotpwd.component.css']
})
export class ForgotpwdComponent implements OnInit {

  constructor(private router:Router,private http:HttpClient) { }
  credentials: any = {UserName:'', Password:''};
  user!: any;
  
  ngOnInit(): void {
  }
  otp!:string;
  data!:string;
  error!: any;

  checkotp(otp:string)
  {
    this.http.get<any>("https://localhost:7067/api/Otp/checkotp/"+otp).subscribe({
      next: (response: any) => {
        this.data="OTP Verified";

      console.log(this.data);
      },
      error: (err: HttpErrorResponse) => {
      console.log(err) ;
      if(err.error.title!=null)
        this.error=err.error.title;
      else
        this.error = err.error;
      }
  })
}
generateotp(email: string,fname:string) 
{
this.http.post<any>("https://localhost:7067/api/Otp/sendemail/"+email+"/"+fname,  {
  headers: new HttpHeaders({ "Content-Type": "application/json"})
})
.subscribe({
  next: (response: any) => {
    this.data="OTP Generated";
    console.log(this.data);
  },
  error: (err: HttpErrorResponse) => {
  console.log(err) ;
  if(err.error.title!=null)
    this.error=err.error.title;
  else
    this.error = err.error;
  }
})
}
getuserbyusername (username:string)
  {
    this.http.get<any>("https://localhost:7067/api/Auth/"+username).subscribe({
      next:  (response: any) => {
        this.user=response;
      console.log(this.user);
      if(this.user!= null){
        console.log(this.user.emailId);
        console.log(this.user.fullName);
        this.generateotp(this.user.emailId,this.user.fullName);
      }
      },
      error: (err: HttpErrorResponse) => {
      console.log(err) ;
      if(err.error.title!=null)
        this.error=err.error.title;
      else
        this.error = err.error;
      }
  })

};

update(password:string,otp:string) {
    this.checkotp(otp);
    console.log(this.data);
    if(this.data=="OTP Verified"){
      this.user.Password=password;
      this.user.Salt = null;
      console.log(this.user.Password);
    this.http.put<RegisterResponse>("https://localhost:7067/api/Auth/updatepassword/"+this.user.UserId, this.user, {
      headers: new HttpHeaders({ "Content-Type": "application/json"})
    })
    .subscribe({
      next:  (response: RegisterResponse) => {
        console.log(response);
        this.router.navigate(["/login"]);
      },
      error: (err: HttpErrorResponse) => {
      console.log(err) ;
      if(err.error.title!=null)
        this.error=err.error;
      else
        this.error = err.error.errors
        console.log(this.error);
      }
    })
    }
    
  
  }
}
