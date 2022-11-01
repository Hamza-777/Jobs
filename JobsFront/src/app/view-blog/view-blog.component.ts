import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Blog } from '../_interfaces/Blog';

@Component({
  selector: 'app-view-blog',
  templateUrl: './view-blog.component.html',
  styleUrls: ['./view-blog.component.css']
})
export class ViewBlogComponent implements OnInit {
  id: number;
  currentBlog: Blog;
  error!: any;

  constructor(private activatedrouter: ActivatedRoute, private http: HttpClient) { 
    this.id = 0;
    this.currentBlog = {
      blogId: 0,
      blogTitle: '',
      blogDescription: '',
      blogContent: '',
      blogTags: '',
      blogCategory: '',
      company: '',
      userId: 1,
    };
    this.activatedrouter.paramMap.subscribe(params => { 
      this.id = Number(params.get('blogId')); 
    });
  }

  ngOnInit(): void {
    this.getBlogs();
  }

  getBlogs = () => {
    this.http.get<Blog>(`https://localhost:7067/api/blogs/${this.id}`)
    .subscribe({
      next: (response: Blog) => {
        this.currentBlog = response;
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
