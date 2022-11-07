import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalerrorhandlerService } from '../globalerrorhandler.service';
import { RegisterModel } from '../_interfaces/register.model';

@Component({
  selector: 'app-showusers',
  templateUrl: './showusers.component.html',
  styleUrls: ['./showusers.component.css']
})
export class ShowusersComponent implements OnInit {

  constructor(private router: Router,private http:HttpClient,private handlerservice:GlobalerrorhandlerService) {}
  users!:any;
  error!:any;
  data!:any;
  ngOnInit(): void {
    this.getusers();
  }

  getusers(){
    this.http.get<any>("https://localhost:7067/api/Admin/getusers").subscribe({
      next:(response:any)=>{
        this.users=response;
        console.log(this.users);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
    }
    })
  }


  deleteuser(id:number){
    this.http.delete<any>("https://localhost:7067/api/Admin/deleteuser/"+id).subscribe({
      next:(response:any)=>{
        this.data="Deleted Successfully!!!!!"
        console.log(this.data);
        window.location.reload();
        
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
    }
    })
  }


}
