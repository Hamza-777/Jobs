import { Component, OnInit } from '@angular/core';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  currentUser: any = null;

  constructor() {
    this.currentUser = localStorage.getItem('jwt')
      ? jwt_decode(localStorage.getItem('jwt')!)
      : null;
    console.log(this.currentUser);
  }

  ngOnInit(): void {}
}
