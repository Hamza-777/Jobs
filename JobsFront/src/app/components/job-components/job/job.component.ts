import { Component, OnInit, Input } from '@angular/core';
import { Job } from '../../../models/Job';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css'],
})
export class JobComponent implements OnInit {
  @Input() job!: Job;

  constructor(private notify: NotificationService) {}

  ngOnInit(): void {}

  donotRedirect() {
    this.notify.showInfo('Login to proceed!');
  }

  loggedIn(): boolean {
    return localStorage.getItem('jwt') ? true : false;
  }
}
