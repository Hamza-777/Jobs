import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { apiresponse } from 'src/app/_interfaces/apiresponse';


@Component({
  selector: 'app-listout-courses',
  templateUrl: './listout-courses.component.html',
  styleUrls: ['./listout-courses.component.css']
})
export class ListoutCoursesComponent implements OnInit {

  courses:Course[]=[];
  error:any;
  
  constructor(private route:ActivatedRoute, private courseService:CoursesService, private router:Router,private handlerservice:GlobalerrorhandlerService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const CategoryName = params.get("courseCategory");
        if(CategoryName){
          this.courseService.getCourseByCategory(CategoryName)
          .subscribe({
            next:(response:apiresponse)=>{
              if (response.message == "") {
                this.error = this.handlerservice.handleError(response.error);
              } else {
                this.courses=response.data;
                console.log(response);
              }
            }
          });
        }
      },error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    })
  }
}
