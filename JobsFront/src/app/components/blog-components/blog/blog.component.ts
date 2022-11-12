import { Component, OnInit, Input } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../../../models/Blog';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { BlogsService } from '../../../services/blog-service/blogs.service';
import { apiresponse } from '../../../models/apiresponse';
import jwt_decode from 'jwt-decode';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css'],
})
export class BlogComponent implements OnInit {
  @Input() blog: Blog;
  error: any;
  currentUser: any;

  constructor(
    private handlerservice: GlobalerrorhandlerService,
    private blogservice: BlogsService,
    private notify: NotificationService,
    private router: Router
  ) {
    this.currentUser = jwt_decode(localStorage.getItem('jwt')!);
  }

  ngOnInit(): void {}

  deleteBlog = (id: any) => {
    this.blogservice.deleteBlog(id).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
          this.notify.showError(response.error);
        } else {
          this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => {
              this.router.navigate(['blogs']);
            });
          this.notify.showSuccess(response.message);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err.error);
        this.notify.showError(err.message);
      },
    });
  };

  storeId = (id: any) => {
    localStorage.setItem('editId', id);
  };
}
