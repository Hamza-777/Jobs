import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../../services/auth-service/auth.service';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { apiresponse } from '../../../models/apiresponse';
import { RegisterModel } from '../../../models/register.model';

@Component({
  selector: 'app-updateuser',
  templateUrl: './updateuser.component.html',
  styleUrls: ['./updateuser.component.css'],
})
export class UpdateuserComponent implements OnInit {
  error!: any;
  data!: string;
  user: RegisterModel = {} as RegisterModel;
  currentUser: any;
  image: any;
  constructor(
    private router: Router,
    private http: HttpClient,
    private handlerservice: GlobalerrorhandlerService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    const token: string = localStorage.getItem('jwt')!;
    console.log(token);
    const tokeninfo: any = jwt_decode(token);
    console.log(tokeninfo);
    this.getuserbyusername(tokeninfo.UserName);
  }
  getuserbyusername(username: string) {
    this.auth.getuserbyusername(username).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.user = response.data;
          console.log(response);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  onFileSelected(event: any) {
    console.log(event);
    this.image = event.target.files[0];
    const fd = new FormData();
    fd.append('image', this.image, this.image.name);
    this.http
      .post('https://api.imgbb.com/1/upload?key=' + environment.imagekey, fd)
      .subscribe({
        next: (response: any) => {
          this.user.photographLink = response['data']['display_url'];
          console.log(this.user.photographLink);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
  }
  editUser = (form: NgForm) => {
    console.log(form);
    if (form.valid) {
      this.auth.edituser(this.user).subscribe({
        next: (response: apiresponse) => {
          if (response.message == '') {
            this.error = this.handlerservice.handleError(response.error);
          } else {
            this.router.navigate(['']);
            console.log(response);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
    }
  };
}
