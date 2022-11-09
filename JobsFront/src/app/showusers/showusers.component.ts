import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AdminService } from '../services/admin-service/admin.service';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';
import { RegisterModel } from '../_interfaces/register.model';

@Component({
  selector: 'app-showusers',
  templateUrl: './showusers.component.html',
  styleUrls: ['./showusers.component.css']
})
export class ShowusersComponent implements OnInit {

  constructor(private router: Router,private http:HttpClient,private handlerservice:GlobalerrorhandlerService,private adminservice:AdminService) {}
  users!:any;
  error!:any;
  data!:any;
  ngOnInit(): void {
    this.getusers();
  }

  getusers(){
    this.adminservice.getusers().subscribe({
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
    this.adminservice.deleteuser(id).subscribe({
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
