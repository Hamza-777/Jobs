import { Component, OnInit } from '@angular/core';
import { TokenService } from '../../../services/token-service/token.service';
import jwt_decode from 'jwt-decode';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(
    public tokenservice: TokenService,
    private notify: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  isUserAuthenticated = (): boolean => {
    const token: string = localStorage.getItem('jwt')!;
    if (token) {
      return true;
    } else {
      return false;
    }
  };

  isAdmin = (): boolean => {
    if (this.isUserAuthenticated()) {
      const token: string = localStorage.getItem('jwt')!;
      const tokeninfo: any = jwt_decode(token);
      if (tokeninfo.Role == 'Admin') {
        return true;
      } else {
        return false;
      }
    }
    return false;
  };

  logOut = () => {
    this.tokenservice.deleteToken();
    this.router.navigate(['login']);
    this.notify.showSuccess('Logged Out Successfully!');
  };
}
