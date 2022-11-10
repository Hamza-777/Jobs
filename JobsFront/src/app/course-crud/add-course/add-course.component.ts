import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { apiresponse } from 'src/app/_interfaces/apiresponse';


@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent implements OnInit {

  addCourseRequest:Course={
    courseId:0,
    courseName:'',
    courseCategory:'',
    courseDescription:'',
    courseAuthor:'',
    courseAmount:0,
    courseImage:'',
    courseVideoURL:''
   };
   error!:any;
  constructor(private courseService:CoursesService,private router:Router,private handlerservice:GlobalerrorhandlerService) { }

  ngOnInit(): void {
  }

  addCourse(){
    this.courseService.addCourse(this.addCourseRequest)
    .subscribe({
      next:(response:apiresponse)=>{
        if (response.message == "") {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.router.navigate(['course-crud']);
          console.log(response);
        }
       
      }, error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    });
    
  }

}
