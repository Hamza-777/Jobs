import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Blog } from '../../../models/Blog';
import jwt_decode from 'jwt-decode';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { BlogsServiceService } from '../../../services/blog-service/blogs-service.service';
import { apiresponse } from '../../../models/apiresponse';

@Component({
  selector: 'app-create-blog',
  templateUrl: './create-blog.component.html',
  styleUrls: ['./create-blog.component.css'],
})
export class CreateBlogComponent implements OnInit {
  editId: any;
  blog: any;
  blogs: Blog[];
  error: any;
  currentUser: any;
  author: any;

  constructor(
    private handlerservice: GlobalerrorhandlerService,
    private blogservice: BlogsServiceService
  ) {
    this.blog = {
      coverImage:
        'https://imaginarytechblog.com/wp-content/uploads/2022/06/150624-The_Current_state_of_blogging_1200x628-01.png',
      blogTitle: '',
      blogContent: '',
      blogTags: '',
      userId: 1,
    };
    this.blogs = [];
    this.editId =
      localStorage.getItem('editId') !== null || undefined
        ? localStorage.getItem('editId')
        : '';

    this.currentUser = jwt_decode(localStorage.getItem('jwt')!);
  }

  ngOnInit(): void {
    this.editId =
      localStorage.getItem('editId') !== null || undefined
        ? localStorage.getItem('editId')
        : '';

    if (this.editId) {
      this.getBlog();
    }
  }

  getBlog = () => {
    this.blogservice.getBlog(this.editId).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.blog = response.data;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  };

  createBlog = (form: NgForm) => {
    if (form.valid) {
      this.blogservice.createBlog(this.blog).subscribe({
        next: (response: apiresponse) => {
          alert(response.message);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
    }
  };

  editBlog = (form: NgForm) => {
    if (form.valid) {
      this.blogservice
        .editBlog(this.editId, this.blog, this.currentUser.UserID)
        .subscribe({
          next: (response: apiresponse) => {
            if (response.message == '') {
              this.error = this.handlerservice.handleError(response.error);
            } else {
              alert(response.message);
            }
          },
          error: (err: HttpErrorResponse) => {
            this.error = this.handlerservice.handleError(err);
          },
        });
    }
    localStorage.removeItem('editId');
  };
}
