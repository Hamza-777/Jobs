import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { LoginModel } from '../../../models/login.model';
import { AuthService } from '../../../services/auth-service/auth.service';
import { TokenService } from '../../../services/token-service/token.service';
import { apiresponse } from '../../../models/apiresponse';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;
  credentials: LoginModel = { userdata: '', password: '' };
  error: any;

  constructor(
    private router: Router,
    private auth: AuthService,
    private tokenservice: TokenService,
    private handlerservice: GlobalerrorhandlerService,
    private notify: NotificationService
  ) {}

  ngOnInit(): void {}

  login = (form: NgForm) => {
    if (form.valid) {
      this.auth.loginuser(this.credentials).subscribe({
        next: (response: apiresponse) => {
          if (response.message == '') {
            this.error = this.handlerservice.handleError(response.error);
          } else {
            this.tokenservice.addToken(response.data.token);
            this.invalidLogin = false;
            this.notify.showSuccess('Logged In Successfullly!!');
            this.router.navigate(['/']);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.invalidLogin = true;
          this.error = this.handlerservice.handleError(err.error);
        },
      });
    }
  };
}
