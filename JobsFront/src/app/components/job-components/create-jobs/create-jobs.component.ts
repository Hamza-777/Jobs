import { Component, OnInit } from '@angular/core';
import { JobsService } from '../../../services/jobs-service/jobs.service';
import { Job } from '../../../models/Job';
import { JobsComponent } from '../jobs/jobs.component';
import { Category } from '../../../models/Category';
import { City } from '../../../models/City';
import { State } from '../../../models/State';
import { NgForm } from '@angular/forms';
import { formatCurrency } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { apiresponse } from '../../../models/apiresponse';

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
    private handlerservice: GlobalerrorhandlerService
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
    console.log(this.categoryList);
    this.getCities();
    this.getCategory();
    this.getStates();
  }

  getStatesFromCities(cityId: number) {
    // If pune map to maharastra
    if (cityId == 4) {
      this.newjob.stateid = this.stateList.find((x) => x.id == 2).id;
    }
    // If delhi map to delhi
    else if (cityId == 6) {
      this.newjob.stateid = this.stateList.find((x) => x.id == 4).id;
    } else {
      this.newjob.stateid = this.stateList.find((x) => x.id == cityId).id;
    }
  }

  createJobs(form: NgForm) {
    if (form.valid) {
      console.log('entered data');
      this.getStatesFromCities(this.newjob.cityid);
      this.newjob.location =
        this.cityList.find((x) => x.id == this.newjob.cityid).name +
        ', ' +
        this.stateList.find((x) => x.id == this.newjob.stateid).name;
      this.jobservice.postJobs(this.newjob).subscribe({
        next: (response: apiresponse) => {
          if (response.message == '') {
            this.error = this.handlerservice.handleError(response.error);
          } else {
            console.log(response);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
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
          console.log(response);
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
          console.log(response);
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
          console.log(response);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  // remove element from list
  private removeObjectWithId(arr, id) {
    const objWithIdIndex = arr.findIndex((obj) => obj.id === id);
    arr.splice(objWithIdIndex, 1);

    return arr;
  }
}
