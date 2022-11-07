import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../../_interfaces/Blog';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class BlogsServiceService {

  constructor(private http: HttpClient) { }

  getBlogs = () => {
    this.http.get<Blog[]>("https://localhost:7067/api/blogs")
    .subscribe({
      next: (response: Blog[]) => {
        return response;
      },
      error: (err: HttpErrorResponse) => {
      if(err.error.title!=null)
        return err.error.title;
      else
        return err.error;
      }
    })
  }

  getBlog = (editId: any) => {
    this.http.get<Blog>(`https://localhost:7067/api/blogs/${editId}`)
    .subscribe({
      next: (response: Blog) => {
        return response;
      },
      error: (err: HttpErrorResponse) => {
      if(err.error.title!=null)
        return err.error.title;
      else
        return err.error;
      }
    })
}

  createBlog = (form: NgForm, blog: Blog, userId: any) => {
    if (form.valid) {
      this.http.post<any>("https://localhost:7067/api/blogs", {...blog, userId: userId}, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: any) => {
          return response;
        },
        error: (err: HttpErrorResponse) => {
        if(err.error.title!=null)
          return err.error.title;
        else
          return err.error;
        }
      })
    }
  }

  editBlog = (form: NgForm, editId: any, blog: Blog, userId: any) => {
    if (form.valid) {
      this.http.put<any>(`https://localhost:7067/api/blogs/${editId}`, {...blog, blogId: editId, userId: userId}, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: any) => {
          return response;
        },
        error: (err: HttpErrorResponse) => {
        console.log(err) ;
        if(err.error.title!=null)
          return err.error.title;
        else
          return err.error;
        }
      })
    }
    localStorage.removeItem("editId");
  }

  deleteBlog = (id: any) => {
    this.http.delete<Blog>(`https://localhost:7067/api/blogs/${id}`)
    .subscribe({
      next: (response: Blog) => {
        return response;
      },
      error: (err: HttpErrorResponse) => {
      if(err.error.title!=null)
        return err.error.title;
      else
        return err.error;
      }
    })
  }
}
