import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from '../_interfaces/register.model';
import { RegisterResponse } from '../_interfaces/registerresponse.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  credentials: RegisterModel = {FullName :'',
  UserName :'',
  Bio :'',
  EmailId :'',
  Password :'',
  MobileNumber : 0,
  PhotographLink :'',
  ResumeLink :'',
  WorkStatus :false,
  CurrentSalary : 0,
  ExpectedSalary :0,
  CurrentLocation :'',
  PreferredLocation :'',
  CompanyName :'',
  RecruiterDescription :'',
  Role :''
  }
  constructor(private router: Router,private http:HttpClient) {}

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
        error: (err: HttpErrorResponse) => console.log(err) 
      })
    }
  }
}
