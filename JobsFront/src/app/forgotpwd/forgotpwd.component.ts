import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';

@Component({
  selector: 'app-forgotpwd',
  templateUrl: './forgotpwd.component.html',
  styleUrls: ['./forgotpwd.component.css']
})
export class ForgotpwdComponent implements OnInit {

  constructor(private router:Router,private http:HttpClient,private handlerservice:GlobalerrorhandlerService) { }
  credentials: any = {UserName:'', Password:''};
  user!: any;
  
  ngOnInit(): void {
  }
  otp!:string;
  data!:string;
  error!: any;

generateotp(email: string,fname:string) 
{
this.http.post<any>(environment.ApiUrl+"Otp/sendemail/"+email+"/"+fname,  {
  headers: new HttpHeaders({ "Content-Type": "application/json"})
})
.subscribe({
  next: (response: any) => {
    this.data="OTP Generated";
    console.log(this.data);
  },
  error: (err: HttpErrorResponse) => {
    this.error = this.handlerservice.handleError(err);
  }
})
}
getuserbyusername (username:string)
  {
    this.http.get<any>(environment.ApiUrl+"Auth/"+username).subscribe({
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
        this.error = this.handlerservice.handleError(err);
      }
  })

}

update(password:string,otp:string) {
    this.http.get<any>(environment.ApiUrl+"Otp/checkotp/"+otp).subscribe({
      next: (response: any) => {
        this.data="OTP Verified";
        console.log(response)
        console.log("status",this.data);        
        console.log(this.user);
        this.http.put<any>(environment.ApiUrl+"Auth/updatepassword/"+this.user.userID, {...this.user,password:password}, {
          headers: new HttpHeaders({ "Content-Type": "application/json"})
        })
        .subscribe({
          next:  (response: any) => {
            console.log(response);
            this.router.navigate(["/login"]);
          },
          error: (err: HttpErrorResponse) => {
            this.error = this.handlerservice.handleError(err);
          }
        })
        }
        
      
      })
    
      }
}
