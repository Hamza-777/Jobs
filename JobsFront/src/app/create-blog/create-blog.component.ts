import { Component, OnInit } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Blog } from '../_interfaces/Blog';
import jwt_decode from 'jwt-decode';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';
import { environment } from 'src/environments/environment';
import { BlogsServiceService } from '../services/blog-service/blogs-service.service';

@Component({
  selector: 'app-create-blog',
  templateUrl: './create-blog.component.html',
  styleUrls: ['./create-blog.component.css'],
})
export class CreateBlogComponent implements OnInit {
  editId: any;
  blog: any;
  blogs: Blog[];
  error!: any;
  currentUser: any;
  author: any;

  constructor(
    private http: HttpClient,
    private handlerservice: GlobalerrorhandlerService,
    private blogservice: BlogsServiceService
  ) {
    this.blog = {
      blogTitle: '',
      blogDescription: '',
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
      next: (response: Blog) => {
        this.blog = response;
        console.log(response);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  };

  createBlog = (form: NgForm) => {
    if (form.valid) {
      this.blogservice.createBlog(this.blog).subscribe({
        next: (response: any) => {
          console.log(response);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
    }
  };

  editBlog = (form: NgForm) => {
    if (form.valid) {
      this.blogservice.editBlog(
        this.blog,
        this.editId,
        this.currentUser.UserID
      );
      this.http
        .put<any>(
          environment.ApiUrl + `blogs/${this.editId}`,
          {
            ...this.blog,
            blogId: this.editId,
            userId: this.currentUser.UserID,
          },
          {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
          }
        )
        .subscribe({
          next: (response: any) => {
            console.log(response);
          },
          error: (err: HttpErrorResponse) => {
            this.error = this.handlerservice.handleError(err);
          },
        });
    }
    localStorage.removeItem('editId');
  };
}
