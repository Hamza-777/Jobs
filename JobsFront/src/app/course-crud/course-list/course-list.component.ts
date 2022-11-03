import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {

  courses:Course[]=[];

  constructor(private coursesService:CoursesService) { }

  ngOnInit(): void {
    
    this.coursesService.getAllCourses()
    .subscribe({
      next:(course)=>{

       
        this.courses=course;
        
        //console.log(this.courses[0]);
       
      },
      error:(response)=>{
        console.log(response);
      }
    })

  }

}
