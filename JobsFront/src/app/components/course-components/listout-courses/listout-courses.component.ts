import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalerrorhandlerService } from 'src/app/services/error-service/globalerrorhandler.service';
import { Course } from '../../../models//course.model';
import { CoursesService } from 'src/app/services/courses-service/courses.service';
import { apiresponse } from 'src/app/models/apiresponse';

@Component({
  selector: 'app-listout-courses',
  templateUrl: './listout-courses.component.html',
  styleUrls: ['./listout-courses.component.css'],
})
export class ListoutCoursesComponent implements OnInit {
  courses: Course[];
  filteredCourses: Course[];
  searchQuery: string;
  error: any;

  constructor(
    private route: ActivatedRoute,
    private courseService: CoursesService,
    private handlerservice: GlobalerrorhandlerService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: () => {
        this.courseService.getAllCourses().subscribe({
          next: (response: apiresponse) => {
            if (response.message == '') {
              this.error = this.handlerservice.handleError(response.error);
            } else {
              this.courses = response.data;
              this.filteredCourses = response.data;
            }
          },
        });
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  filterCourses() {
    this.filteredCourses = this.courses.filter((course) =>
      course.courseName
        .toLocaleLowerCase()
        .includes(this.searchQuery.toLocaleLowerCase())
    );
  }
}
