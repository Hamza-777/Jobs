import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { apiresponse } from 'src/app/models/apiresponse';
import { environment } from 'src/environments/environment';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';

@Component({
  selector: 'app-forgotpwd',
  templateUrl: './forgotpwd.component.html',
  styleUrls: ['./forgotpwd.component.css'],
})
export class ForgotpwdComponent implements OnInit {
  constructor(
    private router: Router,
    private http: HttpClient,
    private handlerservice: GlobalerrorhandlerService
  ) {}
  credentials: any = { UserName: '', Password: '' };
  user!: any;

  ngOnInit(): void {}
  otp!: string;
  data!: string;
  error!: any;

  generateotp(email: string, fname: string) {
    this.http
      .post<apiresponse>(environment.ApiUrl + 'Otp/sendemail/' + email + '/' + fname, {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      })
      .subscribe({
        next: (response: apiresponse) => {
          this.data = 'OTP Generated';
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
  }
  getuserbyusername(username: string) {
    this.http.get<apiresponse>(environment.ApiUrl + 'Auth/' + username).subscribe({
      next: (response: apiresponse) => {
        this.user = response.data;
        if (this.user != null) {
          this.generateotp(this.user.emailId, this.user.fullName);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  update(password: string, otp: string) {
    this.http.get<apiresponse>(environment.ApiUrl + 'Otp/checkotp/' + otp).subscribe({
      next: (response: apiresponse) => {
        this.data = 'OTP Verified';
        this.http
          .put<apiresponse>(
            environment.ApiUrl + 'Auth/updatepassword/' + this.user.userID,
            { ...this.user, password: password },
            {
              headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
            }
          )
          .subscribe({
            next: (response: apiresponse) => {
              this.router.navigate(['/login']);
            },
            error: (err: HttpErrorResponse) => {
              this.error = this.handlerservice.handleError(err);
            },
          });
      },
    });
  }
}
