import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiresponse } from 'src/app/models/apiresponse';
import { LoginModel } from 'src/app/models/login.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}
  loginuser(credentials: LoginModel) {
    return this.http.post<apiresponse>(
      environment.ApiUrl + 'auth/login',
      credentials,
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      }
    );
  }

  verifyotp(otp: string) {
    return this.http.get<any>(environment.ApiUrl + 'Otp/checkotp/' + otp);
  }

  otpgeneration(email: string, fname: string) {
    return this.http.post<any>(
      environment.ApiUrl + 'Otp/sendemail/' + email + '/' + fname,
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      }
    );
  }

  registeruser(user) {
    return this.http.post<any>(environment.ApiUrl + 'auth/register', user, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    });
  }

  getuserbyusername(username) {
    return this.http.get<any>(environment.ApiUrl + 'Auth/' + username);
  }

  edituser(user) {
    return this.http.put<any>(
      environment.ApiUrl + 'Auth/updateuser/' + user.userID,
      user,
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      }
    );
  }
}
