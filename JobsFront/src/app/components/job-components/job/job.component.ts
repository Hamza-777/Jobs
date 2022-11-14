import { Component, OnInit, Input } from '@angular/core';
import { Job } from '../../../models/Job';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { TokenService } from 'src/app/services/token-service/token.service';
import { JobsService } from 'src/app/services/jobs-service/jobs.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css'],
})
export class JobComponent implements OnInit {
  @Input() job!: Job;
  error: any = null;

  constructor(
    private notify: NotificationService,
    public tokenservice: TokenService,
    private router: Router,
    private jobservice: JobsService,
    private handlerservice: GlobalerrorhandlerService
  ) {}

  ngOnInit(): void {}

  donotRedirect() {
    this.notify.showInfo('Login to proceed!');
  }

  deleteJob(id: number) {
    this.jobservice.deleteJobs(id).subscribe({
      next: (response) => {
        this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => {
              this.router.navigate(['jobs']);
            });
        this.notify.showSuccess(response.message);
      },
      error: (errResponse: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(errResponse.error);
      },
    });
  }
}
