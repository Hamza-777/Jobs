import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../../_interfaces/Blog';
import { NgForm } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { GlobalerrorhandlerService } from '../error-service/globalerrorhandler.service';
import jwt_decode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class BlogsServiceService {
  error!:any;
  currentUser: any;
  constructor(private http: HttpClient,private handlerservice:GlobalerrorhandlerService) { 
    this.currentUser = jwt_decode(localStorage.getItem("jwt")!);
  }
  getBlogs(){
    return this.http.get<Blog[]>(environment.ApiUrl+"blogs")
  }

  getBlog(editId){
    return this.http.get<Blog>(environment.ApiUrl+`blogs/${editId}`)
  }
  
  createBlog(blog){
    return this.http.post<any>(environment.ApiUrl+"blogs", {...blog, userId: this.currentUser.UserID}, {
      headers: new HttpHeaders({ "Content-Type": "application/json"})
    })
  }

  editBlog(editId,blog,userId){
    return this.http.put<any>(environment.ApiUrl+`blogs/${editId}`, {...blog, blogId: editId, userId: userId}, {
    headers: new HttpHeaders({ "Content-Type": "application/json"})
    })
  }

  deleteBlog(id){
    return this.http.delete<Blog>(environment.ApiUrl+`blogs/${id}`)
  }
  
//   getBlogs = () => {
//     this.http.get<Blog[]>(environment.ApiUrl+"blogs")
//     .subscribe({
//       next: (response: Blog[]) => {
//         return response;
//       },
//       error: (err: HttpErrorResponse) => {
//         this.error = this.handlerservice.handleError(err);
//       }
//     })
//   }

//   getBlog = (editId: any) => {
//     this.http.get<Blog>(environment.ApiUrl+`blogs/${editId}`)
//     .subscribe({
//       next: (response: Blog) => {
//         return response;
//       },
//       error: (err: HttpErrorResponse) => {
//         this.error = this.handlerservice.handleError(err);
//       }
//     })
// }

//   createBlog = (form: NgForm, blog: Blog, userId: any) => {
//     if (form.valid) {
//       this.http.post<any>(environment.ApiUrl+"blogs", {...blog, userId: userId}, {
//         headers: new HttpHeaders({ "Content-Type": "application/json"})
//       })
//       .subscribe({
//         next: (response: any) => {
//           return response;
//         },
//         error: (err: HttpErrorResponse) => {
//           this.error = this.handlerservice.handleError(err);
//         }
//       })
//     }
//   }

//   editBlog = (form: NgForm, editId: any, blog: Blog, userId: any) => {
//     if (form.valid) {
//       this.http.put<any>(environment.ApiUrl+`blogs/${editId}`, {...blog, blogId: editId, userId: userId}, {
//         headers: new HttpHeaders({ "Content-Type": "application/json"})
//       })
//       .subscribe({
//         next: (response: any) => {
//           return response;
//         },
//         error: (err: HttpErrorResponse) => {
//           this.error = this.handlerservice.handleError(err);
//         }
//       })
//     }
//     localStorage.removeItem("editId");
//   }

//   deleteBlog = (id: any) => {
//     this.http.delete<Blog>(environment.ApiUrl+`blogs/${id}`)
//     .subscribe({
//       next: (response: Blog) => {
//         return response;
//       },
//       error: (err: HttpErrorResponse) => {
//         this.error = this.handlerservice.handleError(err);
//       }
//     })
//   }
}
