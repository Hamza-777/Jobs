import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from '../../../models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { apiresponse } from 'src/app/models/apiresponse';

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrls: ['./edit-course.component.css'],
})
export class EditCourseComponent implements OnInit {
  error!: any;
  courseDetails: Course = {
    courseId: 0,
    courseName: '',
    courseCategory: '',
    courseDescription: '',
    courseAuthor: '',
    courseAmount: 0,
    courseImage: '',
    courseVideoURL: '',
  };
  constructor(
    private route: ActivatedRoute,
    private courseService: CoursesService,
    private router: Router,
    private handlerservice: GlobalerrorhandlerService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('courseId');

        if (id) {
          this.courseService.getCourse(parseInt(id)).subscribe({
            next: (response: apiresponse) => {
              if (response.message == '') {
                this.error = this.handlerservice.handleError(response.error);
              } else {
                this.courseDetails = response.data;
              }
            },
          });
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  updateCourse() {
    this.courseService
      .updateCourse(this.courseDetails.courseId, this.courseDetails)
      .subscribe({
        next: (response: apiresponse) => {
          if (response.message == '') {
            this.error = this.handlerservice.handleError(response.error);
          } else {
            this.router.navigate(['courses']);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
  }

  deleteCourse(courseId: number) {
    this.courseService.deleteCourse(courseId).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.router.navigate(['courses']);
          console.log(response);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }
}
