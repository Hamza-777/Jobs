import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { apiresponse } from 'src/app/_interfaces/apiresponse';


@Component({
  selector: 'app-goto-course',
  templateUrl: './goto-course.component.html',
  styleUrls: ['./goto-course.component.css']
})
export class GotoCourseComponent implements OnInit {

  error!:any;
  courseDetails:Course ={
    courseId :0,
    courseName:'',
    courseCategory:'',
    courseDescription:'',
    courseAuthor:'',
    courseAmount:0,
    courseImage:'',
    courseVideoURL:''

  };

  constructor(private route:ActivatedRoute, private courseService:CoursesService, private router:Router,private handlerservice:GlobalerrorhandlerService) { }

  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next:(params)=>{
        const name = params.get("courseName");
        //this.id = id!

        if(name){
          this.courseService.getCourseByName(name)
          .subscribe({
            next:(response:apiresponse)=>{
              if (response.message == "") {
                this.error = this.handlerservice.handleError(response.error);
              } else {
                this.courseDetails=response.data;
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


