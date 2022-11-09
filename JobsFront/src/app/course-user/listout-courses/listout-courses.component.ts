import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';


@Component({
  selector: 'app-listout-courses',
  templateUrl: './listout-courses.component.html',
  styleUrls: ['./listout-courses.component.css']
})
export class ListoutCoursesComponent implements OnInit {

  courses:Course[]=[];
  error!:any;
  
  constructor(private route:ActivatedRoute, private courseService:CoursesService, private router:Router,private handlerservice:GlobalerrorhandlerService) { }

  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next:(params)=>{
        const CategoryName = params.get("courseCategory");
        //this.id = id!

        if(CategoryName){
          this.courseService.getCourseByCategory(CategoryName)
          .subscribe({
            next:(course)=>{
              this.courses=course;

              console.log(course);

            }
          });

        }
      },error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    })

  }

}
