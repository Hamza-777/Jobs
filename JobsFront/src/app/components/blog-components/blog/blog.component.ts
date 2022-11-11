import { Component, OnInit, Input } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../../../models/Blog';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { BlogsService } from '../../../services/blog-service/blogs.service';
import { apiresponse } from '../../../models/apiresponse';
import jwt_decode from 'jwt-decode';

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
    private blogservice: BlogsService
  ) {
    this.currentUser = jwt_decode(localStorage.getItem('jwt')!);
  }

  ngOnInit(): void {}

  deleteBlog = (id: any) => {
    this.blogservice.deleteBlog(id).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          alert(response.message);
          window.location.reload();
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err.error);
      },
    });
  };

  storeId = (id: any) => {
    localStorage.setItem('editId', id);
  };
}
