
import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  
  courses:Course[]=[];
  

  title = 'mdb-angular-ui-kit-free';
  search:string = "";
  flag:number=0;

  

  items = ['Action', 'Another action', 'Something else here'];
  coursess=["Business Analysis",'Commercial Law','Human Resources','Accounts','Corporate','Tax Planning',"Machine Learning",'Web Development','Software Development'];
  courseCategories=['Technology','Business Management','Finance Management'];
  filteredItems = this.items;
  error: any;

  

  constructor(private coursesService:CoursesService,private route:ActivatedRoute, private courseService:CoursesService, private router:Router,private handlerservice:GlobalerrorhandlerService) { }

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
    })
    
  }
  

  Search(){

    
    this.courses.forEach(element => {

      // if(element.courseCategory.toLowerCase().startsWith(this.search.toLowerCase()) && this.flag==0){
      //   this.flag=-1;
      //   this.search=element.courseCategory;
        
      // }

      // if(element.courseCategory.toLowerCase().endsWith(this.search.toLowerCase()) && this.flag==0){
      //   this.flag=-1;
      //   this.search=element.courseCategory;
      // }

      if(element.courseCategory.toLowerCase().includes(this.search.toLowerCase()) && this.flag==0){
        this.flag=-1;
        this.search=element.courseCategory;
      }

      // if(element.courseName.toLowerCase().startsWith(this.search.toLowerCase()) && this.flag==0){
      //   this.flag=1;
      //   this.search=element.courseName;
      // }

      

      // if(element.courseName.toLowerCase().endsWith(this.search.toLowerCase()) && this.flag==0){
      //   this.flag=1;
      //   this.search=element.courseName;
      // }

      if(element.courseName.toLowerCase().includes(this.search.toLowerCase()) && this.flag==0){
        this.flag=1;
        this.search=element.courseName;
      }
      
      
    });
      
    

    if(this.flag==-1){
      this.router.navigate(['/course-user','listout-courses',this.search])
    }
    else if(this.flag==1)
    {
      this.router.navigate(['/course-user','goto-course',this.search]);
    }
    else
    {
      
       alert("No matches found");
     
    }

                
  
            
    }

   

}


















// @Component({
//   selector: 'app-course',
//   templateUrl: './course.component.html',
//   styleUrls: ['./course.component.css']
// })
// export class CourseComponent implements OnInit {

//   constructor() { }

//   ngOnInit(): void {
//   }

// }



// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-course',
//   templateUrl:  './course.component.html',
//   styleUrls: ['./course.component.css']
// })
// export class AppComponent {
//   title = 'mdb-angular-ui-kit-free';

//   items = ['Action', 'Another action', 'Something else here'];
//   filteredItems = this.items;

//   searchItems(event: any) {
//     const value = event.target.value;

//     this.filterItems(value);
//   }

//   filterItems(value: string) {
//     const filterValue = value.toLowerCase();
//     this.filteredItems = this.items.filter((item: string) =>
//       item.toLowerCase().includes(filterValue)
//     );
//   }
// 
