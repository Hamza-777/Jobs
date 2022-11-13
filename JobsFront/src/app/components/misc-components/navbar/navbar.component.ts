import { Component, OnInit } from '@angular/core';
import { TokenService } from '../../../services/token-service/token.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { ConnectableObservable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  currentUser: any = null;

  constructor(
    public tokenservice: TokenService,
    private notify: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUser = localStorage.getItem('jwt')
      ? jwt_decode(localStorage.getItem('jwt')!)
      : null;
  }

  logOut = () => {
    this.tokenservice.deleteToken();
    this.router.navigate(['login']);
    this.notify.showSuccess('Logged Out Successfully!');
  };
}
