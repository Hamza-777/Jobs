import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses.service';

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrls: ['./edit-course.component.css']
})
export class EditCourseComponent implements OnInit {
  //id!:string
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
  constructor(private route:ActivatedRoute, private courseService:CoursesService, private router:Router) { }
  
  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next:(params)=>{
        const id = params.get("courseId");
        //this.id = id!

        if(id){
          this.courseService.getCourse(parseInt(id))
          .subscribe({
            next:(response)=>{
              this.courseDetails=response;

              console.log(response);

            }
          });

        }
      }
    })
  }

  updateCourse(){
    this.courseService.updateCourse(this.courseDetails.courseId,
      this.courseDetails)
      .subscribe({
        next:(response)=>{
          this.router.navigate(['course-crud']);
        }
      });
  }

  deleteCourse(courseId:number){
    this.courseService.deleteCourse(courseId)
    .subscribe({
      next:(response)=>{
        this.router.navigate(['course-crud']);
      }
    });

  }

}
