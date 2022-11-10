import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { apiresponse } from '../../../models/apiresponse';
import { Course } from 'src/app/models/course.model';

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
    private courseService: CoursesService
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
          response.data.courseVideoURL = response.data.courseVideoURL
            .split('=')[1]
            .split('&')[0];
          this.currentCourse = response.data;
        }
      },
    });
  };
}
