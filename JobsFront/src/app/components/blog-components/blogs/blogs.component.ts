import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../../../models/Blog';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { BlogsService } from '../../../services/blog-service/blogs.service';
import { apiresponse } from '../../../models/apiresponse';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css'],
})
export class BlogsComponent implements OnInit {
  filteredBlogs: Blog[];
  blogs: Blog[];
  error: any;
  searchQuery: string = '';

  constructor(
    private handlerservice: GlobalerrorhandlerService,
    private blogservice: BlogsService
  ) {}

  ngOnInit(): void {
    this.getBlogs();
  }

  getBlogs = () => {
    this.blogservice.getBlogs().subscribe({
      next: (response: apiresponse) => {
        console.log(response);
        this.blogs = response.data;
        this.filteredBlogs = response.data;
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  };

  filterBlogs() {
    if (this.searchQuery[0] === '#') {
      this.filteredBlogs = this.blogs.filter((blog) =>
        blog.blogTags
          .toLocaleLowerCase()
          .includes(this.searchQuery.toLocaleLowerCase().substring(1))
      );
    } else {
      this.filteredBlogs = this.blogs.filter((blog) =>
        blog.blogTitle
          .toLocaleLowerCase()
          .includes(this.searchQuery.toLocaleLowerCase())
      );
    }
  }
}