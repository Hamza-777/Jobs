import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, UrlSerializer } from '@angular/router';
import { environment } from 'src/environments/environment';
import { GlobalerrorhandlerService } from '../globalerrorhandler.service';
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
    private handlerservice: GlobalerrorhandlerService
  ) {
    this.user.workStatus = false;
    this.user.role = 'Applicant';
  }
  checkotp(otp: string) {
    this.http
      .get<any>('https://localhost:7067/api/Otp/checkotp/' + otp)
      .subscribe({
        next: (response: any) => {
          this.data = 'OTP Verified';
          if (this.user.password === this.confirmpassword) {
            this.register(this.form);
          } else {
            alert('Passwords do not match!');
          }
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
  }
  generateotp(email: string, fname: string) {
    this.http
      .post<any>(
        'https://localhost:7067/api/Otp/sendemail/' + email + '/' + fname,
        {
          headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
        }
      )
      .subscribe({
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
      this.http
        .post<any>('https://localhost:7067/api/auth/register', this.user, {
          headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
        })
        .subscribe({
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
