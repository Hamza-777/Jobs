import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from '../../../models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { apiresponse } from '../../../models/apiresponse';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
})
export class CourseComponent implements OnInit {
  courses: Course[] = [];
  title = 'mdb-angular-ui-kit-free';
  search: string = '';
  flag: number = 0;
  coursess = [
    'Business Analysis',
    'Commercial Law',
    'Human Resources',
    'Accounts',
    'Corporate',
    'Tax Planning',
    'Machine Learning',
    'Web Development',
    'Software Development',
  ];
  courseCategories = [
    'Technology',
    'Business Management',
    'Finance Management',
  ];
  error: any;

  constructor(
    private coursesService: CoursesService,
    private route: ActivatedRoute,
    private courseService: CoursesService,
    private router: Router,
    private handlerservice: GlobalerrorhandlerService
  ) {}

  ngOnInit(): void {
    this.coursesService.getAllCourses().subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.courses = response.data;
          console.log(response);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  Search() {
    this.courses.forEach((element) => {
      if (
        element.courseCategory
          .toLowerCase()
          .includes(this.search.toLowerCase()) &&
        this.flag == 0
      ) {
        this.flag = -1;
        this.search = element.courseCategory;
      }
      if (
        element.courseName.toLowerCase().includes(this.search.toLowerCase()) &&
        this.flag == 0
      ) {
        this.flag = 1;
        this.search = element.courseName;
      }
    });

    if (this.flag == -1) {
      this.router.navigate(['/course-user', 'listout-courses', this.search]);
    } else if (this.flag == 1) {
      this.router.navigate(['/course-user', 'goto-course', this.search]);
    } else {
      alert('No matches found');
    }
  }
}
