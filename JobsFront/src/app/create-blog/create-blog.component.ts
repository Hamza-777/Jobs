import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Blog } from '../_interfaces/Blog';

@Component({
  selector: 'app-create-blog',
  templateUrl: './create-blog.component.html',
  styleUrls: ['./create-blog.component.css']
})
export class CreateBlogComponent implements OnInit {
  blog: Blog;
  blogs: Blog[];
  data: any;
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
  }

  createBlog = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<any>("https://localhost:7067/api/blogs", this.blog, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: any) => {
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

}
