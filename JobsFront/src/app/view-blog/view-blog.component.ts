import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../_interfaces/Blog';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';
import { environment } from 'src/environments/environment';
import { BlogsServiceService } from '../services/blog-service/blogs-service.service';

@Component({
  selector: 'app-view-blog',
  templateUrl: './view-blog.component.html',
  styleUrls: ['./view-blog.component.css']
})
export class ViewBlogComponent implements OnInit {
  id: number;
  currentBlog: Blog;
  error!: any;

  constructor(private activatedrouter: ActivatedRoute, private http: HttpClient,private handlerservice:GlobalerrorhandlerService,
    private blogservice:BlogsServiceService) { 
    this.id = 0;
    this.currentBlog = {
      blogId: 0,
      blogTitle: '',
      blogDescription: '',
      blogContent: '',
      blogTags: '',
      blogCategory: '',
      company: '',
      userID: 1,
    };
    this.activatedrouter.paramMap.subscribe(params => { 
      this.id = Number(params.get('blogId')); 
    });
  }

  ngOnInit(): void {
    this.getBlog();
  }

  getBlog = () => {
    this.blogservice.getBlog(this.id)
    .subscribe({
      next: (response: Blog) => {
        this.currentBlog = response;
        console.log(response);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    })
}

}
