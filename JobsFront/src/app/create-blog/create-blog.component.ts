import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Blog } from '../_interfaces/Blog';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-create-blog',
  templateUrl: './create-blog.component.html',
  styleUrls: ['./create-blog.component.css']
})
export class CreateBlogComponent implements OnInit {
  editId: any;
  blog: any;
  blogs: Blog[];
  error!: any;
  currentUser: any;
  author: any;

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
    this.editId = localStorage.getItem("editId") !== null || undefined ? localStorage.getItem("editId") : '';

    this.currentUser = jwt_decode(localStorage.getItem("jwt")!);
  }

  ngOnInit(): void {
    this.editId = localStorage.getItem("editId") !== null || undefined ? localStorage.getItem("editId") : '';

    if(this.editId) {
      this.getBlog();
    }
  }

  getBlog = () => {
    this.http.get<Blog>(`https://localhost:7067/api/blogs/${this.editId}`)
    .subscribe({
      next: (response: Blog) => {
        this.blog = response;
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

  createBlog = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<any>("https://localhost:7067/api/blogs", {...this.blog, userId: this.currentUser.UserID}, {
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

  editBlog = ( form: NgForm) => {
    if (form.valid) {
      this.http.put<any>(`https://localhost:7067/api/blogs/${this.editId}`, {...this.blog, blogId: this.editId, userId: this.currentUser.UserID}, {
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
    localStorage.removeItem("editId");
  }
}