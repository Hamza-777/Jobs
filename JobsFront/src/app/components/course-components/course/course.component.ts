import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, Input, OnInit } from '@angular/core';
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
  @Input() course: Course;

  constructor() {}

  ngOnInit(): void {}
}
