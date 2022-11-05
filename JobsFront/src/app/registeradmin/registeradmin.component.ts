import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { GlobalerrorhandlerService } from '../globalerrorhandler.service';
import { RegisterModel } from '../_interfaces/register.model';
import { RegisterResponse } from '../_interfaces/registerresponse.model';

@Component({
  selector: 'app-registeradmin',
  templateUrl: './registeradmin.component.html',
  styleUrls: ['./registeradmin.component.css']
})
export class RegisteradminComponent implements OnInit {
  otp!:string;
  data!:string;
  error!: any;
  @ViewChild('registeradminForm') form!: NgForm;
  credentials: RegisterModel = {} as RegisterModel
  constructor(private router: Router,private http:HttpClient,private handlerservice:GlobalerrorhandlerService) 
  {
    this.credentials.Role = "Admin"
  }
  checkotp(otp:string)
  {
    this.http.get<any>("https://localhost:7067/api/Otp/checkotp/"+otp).subscribe({
      next: (response: any) => {
        this.data="OTP Verified";
      console.log(this.data);
      this.register(this.form);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
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
  error: (err: HttpErrorResponse) => {this.error = this.handlerservice.handleError(err);}
})

}
  ngOnInit(): void {
  }
  register = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<RegisterResponse>("https://localhost:7067/api/Admin/RegisterAdmin", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: RegisterResponse) => {
          console.log(response);
          this.router.navigate([""]);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        }
      })
    }
  }
}
