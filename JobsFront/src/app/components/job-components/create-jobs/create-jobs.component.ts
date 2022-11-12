import { Component, OnInit } from '@angular/core';
import { JobsService } from '../../../services/jobs-service/jobs.service';
import { Category } from '../../../models/Category';
import { City } from '../../../models/City';
import { State } from '../../../models/State';
import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { apiresponse } from '../../../models/apiresponse';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-jobs',
  templateUrl: './create-jobs.component.html',
  styleUrls: ['./create-jobs.component.css'],
})
export class CreateJobsComponent implements OnInit {
  newjob: any;
  categoryList: Category[];
  cityList: City[];
  stateList: State[];
  error: any;

  constructor(
    private jobservice: JobsService,
    private handlerservice: GlobalerrorhandlerService,
    private notify: NotificationService,
    private router: Router
  ) {
    this.newjob = {
      title: '',
      description: '',
      redirect_url: '',
      salary_max: 0,
      salary_min: 0,
      company: '',
      location: '',
      created: Date().toString(),
      categoryid: 0,
      stateid: 0,
      cityid: 0,
    };
  }

  ngOnInit(): void {
    this.getCities();
    this.getCategory();
    this.getStates();
  }

  getStatesFromCities(cityId: number) {
    if (cityId == 4) {
      this.newjob.stateid = this.stateList.find((x) => x.id == 2).id;
    } else if (cityId == 6) {
      this.newjob.stateid = this.stateList.find((x) => x.id == 4).id;
    } else {
      this.newjob.stateid = this.stateList.find((x) => x.id == cityId).id;
    }
  }

  createJobs(form: NgForm) {
    if (form.valid) {
      this.getStatesFromCities(this.newjob.cityid);
      this.newjob.location =
        this.cityList.find((x) => x.id == this.newjob.cityid).name +
        ', ' +
        this.stateList.find((x) => x.id == this.newjob.stateid).name;
      this.jobservice.postJobs(this.newjob).subscribe({
        next: (response: apiresponse) => {
          if (response.message == '') {
            this.error = this.handlerservice.handleError(response.error);
            this.notify.showError(response.error);
          } else {
            this.router.navigate(['jobs']);
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

  getCities() {
    this.jobservice.getAllCity().subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.cityList = this.removeObjectWithId(response.data, 5);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  getCategory() {
    this.jobservice.getAllCategory().subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.categoryList = response.data;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  getStates() {
    this.jobservice.getAllState().subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.stateList = this.removeObjectWithId(response.data, 5);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  private removeObjectWithId(arr, id) {
    const objWithIdIndex = arr.findIndex((obj) => obj.id === id);
    arr.splice(objWithIdIndex, 1);

    return arr;
  }
}
