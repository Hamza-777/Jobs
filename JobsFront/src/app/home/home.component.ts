import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { TokenService } from '../services/token-service/token.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private http:HttpClient,public tokenservice:TokenService) { }
  // token_authorize_test()
  // {
  //   //Get-JWT Token again
  //   const token: string =localStorage.getItem("jwt")!;
  //   console.log(token);
  //   const tokeninfo:any = jwt_decode(token);
  //   console.log(tokeninfo);
  //   this.http.get("https://localhost:7067/WeatherForecast")
  //   .subscribe({
  //     next: (result: any) => console.log(result),
  //     error: (err: HttpErrorResponse) => console.log(err)
  // })
  // }
  ngOnInit(): void {
    //this.token_authorize_test();
}

  logOut = () => {
    this.tokenservice.deleteToken();
  }
}
