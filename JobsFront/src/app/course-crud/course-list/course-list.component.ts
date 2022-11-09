import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {

  courses:Course[]=[];
  error!:any;
  constructor(private coursesService:CoursesService,private handlerservice:GlobalerrorhandlerService) { }

  ngOnInit(): void {
    
    this.coursesService.getAllCourses()
    .subscribe({
      next:(course)=>{

       
        this.courses=course;
        
        //console.log(this.courses[0]);
       
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    });

  }

}
