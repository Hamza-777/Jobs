import { Component, OnInit } from '@angular/core';
import { TokenService } from '../services/token-service/token.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private tokenservice: TokenService) {}

  ngOnInit(): void {}

  logOut = () => {
    this.tokenservice.deleteToken();
  };
}
