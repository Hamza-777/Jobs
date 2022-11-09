import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Course } from '../../models/course.model';
import { observable, Observable } from 'rxjs';
import { AddCourseComponent } from '../../course-crud/add-course/add-course.component';
import {GotoCourseComponent } from '../../course-user/goto-course/goto-course.component';
import { ListoutCoursesComponent } from '../../course-user/listout-courses/listout-courses.component';


@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  
  courseApiUrl:string=environment.ApiUrl;
  constructor(private http:HttpClient) { }

  getAllCourses():Observable<Course[]>{
    return this.http.get<Course[]>(environment.ApiUrl+'Courses');
  }

  addCourse(addCourseRequest:Course):Observable<Course>{
    addCourseRequest.courseId=0;
    return this.http.post<Course>(environment.ApiUrl+'Courses',addCourseRequest);
  }

  getCourse(courseId:number) : Observable<Course>{
    return this.http.get<Course>(environment.ApiUrl+'Courses'+courseId);
  }

  getCourseByName(courseName:string) : Observable<Course>{
    return this.http.get<Course>(environment.ApiUrl+'Courses/name?name='+courseName);
  }
  
  getCourseByCategory(courseCategory:string) : Observable<Course[]>{
    return this.http.get<Course[]>(environment.ApiUrl+'Courses/CategoryName?CategoryName='+courseCategory);
  }

  updateCourse(courseId:number,updateCourseRequest:Course):
  Observable<Course>{
    return this.http.put<Course>(environment.ApiUrl+'Courses/'+courseId,updateCourseRequest);
  }

  deleteCourse(courseId:number) : Observable<Course>{
    return this.http.delete<Course>(environment.ApiUrl+'Courses/'+courseId);
  }

}
