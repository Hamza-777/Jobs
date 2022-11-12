import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AdminService } from '../../../services/admin-service/admin.service';
import { AuthService } from '../../../services/auth-service/auth.service';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { apiresponse } from '../../../models/apiresponse';
import { RegisterModel } from '../../../models/register.model';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-registeradmin',
  templateUrl: './registeradmin.component.html',
  styleUrls: ['./registeradmin.component.css'],
})
export class RegisteradminComponent implements OnInit {
  otp!: string;
  data!: string;
  error!: any;
  @ViewChild('registeradminForm') form!: NgForm;
  user: RegisterModel = {} as RegisterModel;
  image: any;

  constructor(
    private router: Router,
    private http: HttpClient,
    private handlerservice: GlobalerrorhandlerService,
    private auth: AuthService,
    private admin: AdminService,
    private notify: NotificationService
  ) {
    this.user.role = 'Admin';
  }

  checkotp(otp: string) {
    this.auth.verifyotp(otp).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
          this.notify.showError(response.error);
        } else {
          this.data = response.message;
          this.notify.showSuccess(response.message);
          this.register(this.form);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
        this.notify.showError(err.message);
      },
    });
  }

  generateotp(email: string, fname: string) {
    this.auth.otpgeneration(email, fname).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
          this.notify.showError(response.error);
        } else {
          this.data = response.message;
          this.notify.showSuccess(response.message);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
        this.notify.showError(err.message);
      },
    });
  }

  ngOnInit(): void {}
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
          this.notify.showError(err.message);
        },
      });
  }

  register = (form: NgForm) => {
    if (form.valid) {
      this.admin.adminregistration(this.user).subscribe({
        next: (response: apiresponse) => {
          if (response.message == '') {
            this.error = this.handlerservice.handleError(response.error);
            this.notify.showError(response.error);
          } else {
            this.router.navigate(['']);
            this.notify.showSuccess(response.message);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
          this.notify.showError(err.message);
        },
      });
    }
  };
}
