import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../../../models/Blog';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { environment } from 'src/environments/environment';
import { BlogsServiceService } from '../../../services/blog-service/blogs-service.service';
import { apiresponse } from '../../../models/apiresponse';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css'],
})
export class BlogsComponent implements OnInit {
  blogs: Blog[];
  error: any;

  constructor(
    private http: HttpClient,
    private handlerservice: GlobalerrorhandlerService,
    private blogservice: BlogsServiceService
  ) {
    this.blogs = [];
  }

  ngOnInit(): void {
    this.getBlogs();
  }

  getBlogs = () => {
    this.blogservice.getBlogs().subscribe({
      next: (response: apiresponse) => {
        this.blogs = response.data;
        console.log(response);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  };
}
