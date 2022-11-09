import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../_interfaces/Blog';
import jwt_decode from 'jwt-decode';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';
import { environment } from 'src/environments/environment';
import { BlogsServiceService } from '../services/blog-service/blogs-service.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  @Input() blog!: Blog;
  error!: any;
  currentUser: any;

  constructor(private http: HttpClient,private handlerservice:GlobalerrorhandlerService,private blogservice:BlogsServiceService) { 
    this.currentUser = jwt_decode(localStorage.getItem("jwt")!);
  }

  ngOnInit(): void {
  }

  deleteBlog = (id: any) => {
    this.blogservice.deleteBlog(id)
    .subscribe({
      next: (response: Blog) => {
        console.log(response);
        window.location.reload();
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    })
  }

  storeId = (id: any) => {
    localStorage.setItem("editId", id);
  }

}
