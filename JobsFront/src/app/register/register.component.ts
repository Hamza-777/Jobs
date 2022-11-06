import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, UrlSerializer } from '@angular/router';
import { environment } from 'src/environments/environment';
import { GlobalerrorhandlerService } from '../globalerrorhandler.service';
import { RegisterModel } from '../_interfaces/register.model';
import { RegisterResponse } from '../_interfaces/registerresponse.model';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit 
{
  otp!:string;
  data!:string;
  error!: any;
  image!: any ;
  @ViewChild('registerForm') form!: NgForm;
  credentials: RegisterModel = {} as RegisterModel
  constructor(private router: Router,private http:HttpClient,private handlerservice:GlobalerrorhandlerService) 
  {
    this.credentials.WorkStatus = false;
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
onFileSelected(event:any)
{
   console.log(event);
   this.image = event.target.files[0]
   const fd = new FormData();
   fd.append('image',this.image,this.image.name)
   this.http.post('https://api.imgbb.com/1/upload?key='+environment.imagekey,fd).subscribe({
    next: (response: any) => {
      this.credentials.PhotographLink = response['data']['display_url'];
      console.log(this.credentials.PhotographLink);
    },
    error: (err: HttpErrorResponse) => {
      this.error = this.handlerservice.handleError(err);
    }
  })
}
  ngOnInit(): void {
  }
  register = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<RegisterResponse>("https://localhost:7067/api/auth/register", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: RegisterResponse) => {
          console.log(response);
          this.router.navigate(["/login"]);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        }
      })
    }
  }
}
