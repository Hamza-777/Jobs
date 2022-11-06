import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { environment } from 'src/environments/environment';
import { GlobalerrorhandlerService } from '../globalerrorhandler.service';
import { RegisterModel } from '../_interfaces/register.model';

@Component({
  selector: 'app-updateuser',
  templateUrl: './updateuser.component.html',
  styleUrls: ['./updateuser.component.css']
})
export class UpdateuserComponent implements OnInit {
  error!: any;
  data!:string;
  user: RegisterModel = {} as RegisterModel;
  currentUser: any;
  image: any;
  constructor(private router: Router,private http: HttpClient,private handlerservice:GlobalerrorhandlerService) 
  {

  }


  ngOnInit(): void 
  {
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
        this.error = this.handlerservice.handleError(err);
      }
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
      this.user.photographLink = response['data']['display_url'];
      console.log(this.user.photographLink);
    },
    error: (err: HttpErrorResponse) => {
      this.error = this.handlerservice.handleError(err);
    }
  })
}
  editUser = ( form: NgForm) => {
    if (form.valid) {
      this.http.put<any>("https://localhost:7067/api/Auth/updateuser/"+this.user.userId , this.user, {
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


