import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses.service';


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
    courseAmount:0
   };

  constructor(private courseService:CoursesService,private router:Router) { }

  ngOnInit(): void {
  }

  addCourse(){
    this.courseService.addCourse(this.addCourseRequest)
    .subscribe({
      next:(course)=>{
        console.log(course);
        this.router.navigate(['course-crud']);
      }
    });
    
  }

}
