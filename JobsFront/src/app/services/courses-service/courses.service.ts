import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Course } from '../../models/course.model';
import { observable, Observable } from 'rxjs';
import { AddCourseComponent } from '../../course-crud/add-course/add-course.component';
import {GotoCourseComponent } from '../../course-user/goto-course/goto-course.component';
import { ListoutCoursesComponent } from '../../course-user/listout-courses/listout-courses.component';
import { apiresponse } from 'src/app/_interfaces/apiresponse';


@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  
  courseApiUrl:string=environment.ApiUrl;
  constructor(private http:HttpClient) { }

  getAllCourses(){
    return this.http.get<apiresponse>(environment.ApiUrl+'Courses');
  }

  addCourse(addCourseRequest:Course):Observable<apiresponse>{
    addCourseRequest.courseId=0;
    return this.http.post<apiresponse>(environment.ApiUrl+'Courses',addCourseRequest);
  }

  getCourse(courseId:number) : Observable<apiresponse>{
    return this.http.get<apiresponse>(environment.ApiUrl+'Courses/'+ courseId);
  }

  getCourseByName(courseName:string) : Observable<apiresponse>{
    return this.http.get<apiresponse>(environment.ApiUrl+'Courses/name?name='+courseName);
  }
  
  getCourseByCategory(courseCategory:string) : Observable<apiresponse>{
    return this.http.get<apiresponse>(environment.ApiUrl+'Courses/CategoryName?CategoryName='+courseCategory);
  }

  updateCourse(courseId:number,updateCourseRequest:Course):
  Observable<apiresponse>{
    return this.http.put<apiresponse>(environment.ApiUrl+'Courses/'+courseId,updateCourseRequest);
  }

  deleteCourse(courseId:number) : Observable<apiresponse>{
    return this.http.delete<apiresponse>(environment.ApiUrl+'Courses/'+courseId);
  }

}
