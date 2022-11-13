import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../../services/auth-service/auth.service';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { apiresponse } from '../../../models/apiresponse';
import { RegisterModel } from '../../../models/register.model';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  otp: string;
  data: string;
  error: any;
  image: any;
  confirmpassword: string;
  @ViewChild('registerForm') form!: NgForm;
  user: RegisterModel = {} as RegisterModel;

  constructor(
    private router: Router,
    private http: HttpClient,
    private handlerservice: GlobalerrorhandlerService,
    private auth: AuthService,
    private notify: NotificationService
  ) {
    this.user.workStatus = false;
    this.user.role = 'Applicant';
  }

  ngOnInit(): void {}

  checkotp(otp: string) {
    this.auth.verifyotp(otp).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.notify.showError(response.error);
        } else {
          this.data = response.message;
          this.notify.showSuccess(response.message);
          this.register(this.form);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  generateotp(email: string, fname: string) {
    this.auth.otpgeneration(email, fname).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.notify.showError(response.error);
        } else {
          this.data = response.message;
          this.notify.showSuccess(response.message);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  onFileSelected(event: any) {
    this.image = event.target.files[0];
    const fd = new FormData();
    fd.append('image', this.image, this.image.name);
    this.http
      .post('https://api.imgbb.com/1/upload?key=' + environment.imagekey, fd)
      .subscribe({
        next: (response: any) => {
          this.user.photographLink = response['data']['display_url'];
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
  }

  register = (form: NgForm) => {
    if (form.valid) {
      this.auth.registeruser(this.user).subscribe({
        next: (response: apiresponse) => {
          if (response.message == '') {
            this.notify.showError(response.error);
          } else {
            this.router.navigate(['/login']);
            this.notify.showSuccess(response.message);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
    }
  };
}
