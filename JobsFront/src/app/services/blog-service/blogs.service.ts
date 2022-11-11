import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { apiresponse } from 'src/app/models/apiresponse';
@Injectable({
  providedIn: 'root',
})
export class BlogsService {
  error!: any;
  currentUser: any;
  constructor(private http: HttpClient) {}
  getBlogs() {
    return this.http.get<apiresponse>(environment.ApiUrl + 'Blogs');
  }

  getBlog(editId) {
    return this.http.get<apiresponse>(environment.ApiUrl + `Blogs/${editId}`);
  }

  createBlog(blog, id) {
    return this.http.post<apiresponse>(
      environment.ApiUrl + 'Blogs',
      { ...blog, userId: id },
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      }
    );
  }

  editBlog(editId, blog, userId) {
    return this.http.put<apiresponse>(
      environment.ApiUrl + `Blogs/${editId}`,
      { ...blog, blogId: editId, userId: userId },
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      }
    );
  }

  deleteBlog(id) {
    return this.http.delete<apiresponse>(environment.ApiUrl + `Blogs/${id}`);
  }
}
