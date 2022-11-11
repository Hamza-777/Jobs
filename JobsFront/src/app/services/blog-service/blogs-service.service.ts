import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import jwt_decode from 'jwt-decode';
import { apiresponse } from 'src/app/models/apiresponse';
@Injectable({
  providedIn: 'root',
})
export class BlogsServiceService {
  error!: any;
  currentUser: any;
  constructor(private http: HttpClient) {
    this.currentUser = jwt_decode(localStorage.getItem('jwt')!);
  }
  getBlogs() {
    return this.http.get<apiresponse>(environment.ApiUrl + 'blogs');
  }

  getBlog(editId) {
    return this.http.get<apiresponse>(environment.ApiUrl + `blogs/${editId}`);
  }

  createBlog(blog) {
    return this.http.post<apiresponse>(
      environment.ApiUrl + 'blogs',
      { ...blog, userId: this.currentUser.UserID },
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      }
    );
  }

  editBlog(editId, blog, userId) {
    return this.http.put<apiresponse>(
      environment.ApiUrl + `blogs/${editId}`,
      { ...blog, blogId: editId, userId: userId },
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      }
    );
  }

  deleteBlog(id) {
    return this.http.delete<apiresponse>(environment.ApiUrl + `blogs/${id}`);
  }
}
