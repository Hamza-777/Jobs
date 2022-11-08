import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalerrorhandlerService } from '../globalerrorhandler.service';

@Component({
  selector: 'app-showuserbyid',
  templateUrl: './showuserbyid.component.html',
  styleUrls: ['./showuserbyid.component.css']
})

export class ShowuserbyidComponent implements OnInit {
  id!:number;
  user!:any;
  error!:any;
  constructor(private activatedrouter: ActivatedRoute,private router: Router,private http:HttpClient,private handlerservice:GlobalerrorhandlerService) {
    this.activatedrouter.paramMap.subscribe(params => { 
      this.id = Number(params.get('id')); 
      console.log(this.id);
    });
  }

  ngOnInit(): void {
    this.getuserbyid();
  }

  getuserbyid(){
    this.http.get<any>("https://localhost:7067/api/Admin/getuserbyid/"+this.id).subscribe({
      next:(response:any)=>{
        this.user=response;
        console.log(this.user);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
    }
    })
  }
}

