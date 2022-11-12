import { Component, OnInit } from '@angular/core';
import { TokenService } from '../../../services/token-service/token.service';
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

  logOut = () => {
    this.tokenservice.deleteToken();
    this.router.navigate(['login']);
    this.notify.showSuccess('Logged Out Successfully!');
  };
}
