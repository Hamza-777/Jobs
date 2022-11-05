import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses.service';


@Component({
  selector: 'app-goto-course',
  templateUrl: './goto-course.component.html',
  styleUrls: ['./goto-course.component.css']
})
export class GotoCourseComponent implements OnInit {

  courseDetails:Course ={
    courseId :0,
    courseName:'',
    courseCategory:'',
    courseDescription:'',
    courseAuthor:'',
    courseAmount:0

  };
  constructor(private route:ActivatedRoute, private courseService:CoursesService, private router:Router) { }

  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next:(params)=>{
        const name = params.get("courseName");
        //this.id = id!

        if(name){
          this.courseService.getCourseByName(name)
          .subscribe({
            next:(response)=>{
              this.courseDetails=response;

              console.log(response);
              console.log(this.courseDetails);

            }
          });

        }
      }
    })
  }

  }


