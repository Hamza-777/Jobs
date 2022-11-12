import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../../../models/Blog';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { BlogsService } from '../../../services/blog-service/blogs.service';
import { apiresponse } from '../../../models/apiresponse';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-view-blog',
  templateUrl: './view-blog.component.html',
  styleUrls: ['./view-blog.component.css'],
})
export class ViewBlogComponent implements OnInit {
  id: number;
  currentBlog: Blog;
  error!: any;
  currentUser: any;

  constructor(
    private activatedrouter: ActivatedRoute,
    private handlerservice: GlobalerrorhandlerService,
    private blogservice: BlogsService,
    private notify: NotificationService,
    private router: Router
  ) {
    this.id = 0;
    this.currentBlog = {
      blogId: 0,
      coverImage:
        'https://imaginarytechblog.com/wp-content/uploads/2022/06/150624-The_Current_state_of_blogging_1200x628-01.png',
      blogTitle: '',
      blogContent: '',
      blogTags: '',
      userID: 1,
    };
    this.activatedrouter.paramMap.subscribe((params) => {
      this.id = Number(params.get('blogId'));
    });
    this.currentUser = jwt_decode(localStorage.getItem('jwt')!);
  }

  ngOnInit(): void {
    this.getBlog();
  }

  getBlog = () => {
    this.blogservice.getBlog(this.id).subscribe({
      next: (response: apiresponse) => {
        this.currentBlog = response.data;
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  };

  deleteBlog = (id: any) => {
    this.blogservice.deleteBlog(id).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
          this.notify.showError(response.message);
        } else {
          this.router.navigate(['blogs']);
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
