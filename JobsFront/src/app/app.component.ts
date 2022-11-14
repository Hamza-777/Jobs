import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { apiresponse } from './models/apiresponse';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'JobsFront';
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.http.get<any>(environment.ApiUrl+'Otp/clearotp/').subscribe({
      next: (response: apiresponse) => {
        console.log('OTPS cleared');
      },
    });
  }
}
