import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { GlobalerrorhandlerService } from '../globalerrorhandler.service';
import { RegisterModel } from '../_interfaces/register.model';

@Component({
  selector: 'app-updateuser',
  templateUrl: './updateuser.component.html',
  styleUrls: ['./updateuser.component.css']
})
export class UpdateuserComponent implements OnInit {
  error!: any;
  user!:any
  data!:string;
  credentials: RegisterModel = {} as RegisterModel
  currentUser: any;
  constructor(private router: Router,private http: HttpClient,private handlerservice:GlobalerrorhandlerService) 
  {

  }

  initialize_token()
  {
    const token: string =localStorage.getItem("jwt")!;
    console.log(token);
    const tokeninfo:any = jwt_decode(token);
    console.log(tokeninfo);
    this.user = tokeninfo;
    if(this.user.WorkStatus == "True")
      this.user.WorkStatus = true;
    else
      this.user.WorkStatus = false;
    this.currentUser = tokeninfo
  }
  ngOnInit(): void 
  {
    //this.initialize_token();
    const token: string =localStorage.getItem("jwt")!;
    console.log(token);
    const tokeninfo:any = jwt_decode(token);
    console.log(tokeninfo);
    this.getuserbyusername(tokeninfo.UserName)
  }
  getuserbyusername (username:string)
  {
    this.http.get<any>("https://localhost:7067/api/Auth/"+username).subscribe({
      next:  (response: any) => {
        this.user=response;
      console.log(this.user);
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
  editUser = ( form: NgForm) => {
    if (form.valid) {
      this.http.put<any>("https://localhost:7067/api/Auth/updateuser/"+this.user.userID , this.user, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: RegisterModel) => {
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


