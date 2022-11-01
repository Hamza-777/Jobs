import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Blog } from '../_interfaces/Blog';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css']
})
export class BlogsComponent implements OnInit {
  blog: Blog;
  blogs: Blog[];
  error!: any;

  constructor(private http: HttpClient) {
    this.blog = {
      blogTitle: '',
      blogDescription: '',
      blogContent: '',
      blogTags: '',
      blogCategory: '',
      company: '',
      userId: 1,
    };
    this.blogs = [];
   }

  ngOnInit(): void {
    this.getBlogs();
  }

  getBlogs = () => {
      this.http.get<Blog[]>("https://localhost:7067/api/blogs")
      .subscribe({
        next: (response: Blog[]) => {
          this.blogs = response;
          console.log(response);
        },
        error: (err: HttpErrorResponse) => {
        console.log(err) ;
        if(err.error.title!=null)
          this.error=err.error.title;
        else
          this.error = err.error;
        }
      })
  }

}
