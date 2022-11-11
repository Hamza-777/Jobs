import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'JobsFront';
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.http.get<any>('https://localhost:7067/api/Otp/clearotp/').subscribe({
      next: (response: any) => {
        console.log('OTPS cleared');
      },
    });
  }
}
