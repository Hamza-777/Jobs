import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Course } from '../models/course.model';
import { observable, Observable } from 'rxjs';
import { AddCourseComponent } from '../course-crud/add-course/add-course.component';
import {GotoCourseComponent } from '../course-user/goto-course/goto-course.component';


@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  
  courseApiUrl:string=environment.courseApiUrl;
  constructor(private http:HttpClient) { }

  getAllCourses():Observable<Course[]>{
    return this.http.get<Course[]>(this.courseApiUrl + '/api/Courses');

  }

  addCourse(addCourseRequest:Course):Observable<Course>{
    addCourseRequest.courseId=0;
    return this.http.post<Course>(this.courseApiUrl + '/api/Courses',addCourseRequest);
  }

  getCourse(courseId:number) : Observable<Course>{
    return this.http.get<Course>(this.courseApiUrl + '/api/Courses/'+courseId);
  }

  getCourseByName(courseName:string) : Observable<Course>{
    return this.http.get<Course>(this.courseApiUrl + '/api/Courses/'+courseName);
  }

  updateCourse(courseId:number,updateCourseRequest:Course):
  Observable<Course>{
    return this.http.put<Course>(this.courseApiUrl + '/api/Courses/'+courseId,updateCourseRequest);
  }

  deleteCourse(courseId:number) : Observable<Course>{
    return this.http.delete<Course>(this.courseApiUrl + '/api/Courses/'+courseId);
  }

}
