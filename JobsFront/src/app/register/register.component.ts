import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, UrlSerializer } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth-service/auth.service';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';
import { RegisterModel } from '../_interfaces/register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  otp!: string;
  data!: string;
  error!: any;
  image!: any;
  confirmpassword!: string;

  @ViewChild('registerForm') form!: NgForm;
  user: RegisterModel = {} as RegisterModel;
  constructor(
    private router: Router,
    private http: HttpClient,
    private handlerservice: GlobalerrorhandlerService,
    private auth: AuthService
  ) {
    this.user.workStatus = false;
    this.user.role = 'Applicant';
  }
  checkotp(otp: string) {
    this.auth.verifyotp(otp).subscribe({
      next: (response: any) => {
        this.data = 'OTP Verified';
        console.log(this.data);
        this.register(this.form);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }
  generateotp(email: string, fname: string) {
    this.auth.otpgeneration(email, fname).subscribe({
      next: (response: any) => {
        this.data = 'OTP Generated';
        console.log(this.data);
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
  ngOnInit(): void {}
  register = (form: NgForm) => {
    if (form.valid) {
      this.auth.registeruser(this.user).subscribe({
        next: (response: any) => {
          console.log(response);
          this.router.navigate(['/login']);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
    }
  };
}
