import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from '../../../models/course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { apiresponse } from 'src/app/models/apiresponse';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css'],
})
export class AddCourseComponent implements OnInit {
  addCourseRequest: Course = {
    courseId: 0,
    courseName: '',
    courseCategory: '',
    courseDescription: '',
    courseAuthor: '',
    courseImage: '',
    courseVideoURL: '',
  };
  error!: any;

  constructor(
    private courseService: CoursesService,
    private router: Router,
    private handlerservice: GlobalerrorhandlerService,
    private notify: NotificationService
  ) {}

  ngOnInit(): void {}

  addCourse() {
    this.courseService.addCourse(this.addCourseRequest).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
          this.notify.showError(response.error);
        } else {
          this.router.navigate(['courses']);
          this.notify.showSuccess(response.message);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
        this.notify.showError(err.message);
      },
    });
  }
}
