import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../_interfaces/Blog';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  @Input() blog!: Blog;
  error!: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  deleteBlog = (id: any) => {
    this.http.delete<Blog>(`https://localhost:7067/api/blogs/${id}`)
    .subscribe({
      next: (response: Blog) => {
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

  storeId = (id: any) => {
    localStorage.setItem("editId", id);
  }

}
