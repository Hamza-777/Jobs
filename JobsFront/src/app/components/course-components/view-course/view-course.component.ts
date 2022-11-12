import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { TokenService } from '../../../services/token-service/token.service';
import { apiresponse } from '../../../models/apiresponse';
import { Course } from 'src/app/models/course.model';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-view-course',
  templateUrl: './view-course.component.html',
  styleUrls: ['./view-course.component.css'],
})
export class ViewCourseComponent implements OnInit {
  id: number;
  currentCourse: Course;
  error!: any;

  constructor(
    private activatedrouter: ActivatedRoute,
    private handlerservice: GlobalerrorhandlerService,
    private courseService: CoursesService,
    public tokenservice: TokenService,
    private notify: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedrouter.paramMap.subscribe((params) => {
      this.id = Number(params.get('courseId'));
    });

    this.getCourse();
  }

  getCourse = () => {
    this.courseService.getCourse(this.id).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          response.data.courseVideoURL = this.getVideoId(
            response.data.courseVideoURL
          );
          this.currentCourse = response.data;
        }
      },
    });
  };

  getVideoId(videoUrl: string): string {
    let videoId =
      videoUrl.split('.')[1][0] === 'b'
        ? videoUrl.split('.')[1].split('/')[1]
        : videoUrl.split('=')[1].split('&')[0];

    return videoId;
  }

  deleteCourse(courseId: number) {
    this.courseService.deleteCourse(courseId).subscribe({
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
