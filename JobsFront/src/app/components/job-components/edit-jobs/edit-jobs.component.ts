import { formatCurrency } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { JobsService } from '../../../services/jobs-service/jobs.service';
import { apiresponse } from '../../../models/apiresponse';
import { Category } from '../../../models/Category';
import { City } from '../../../models/City';
import { Job } from '../../../models/Job';
import { State } from '../../../models/State';

@Component({
  selector: 'app-edit-jobs',
  templateUrl: './edit-jobs.component.html',
  styleUrls: ['./edit-jobs.component.css'],
})
export class EditJobsComponent implements OnInit {
  newjob: Job;
  categoryList: Category[];
  cityList: City[];
  stateList: State[];
  error!: any;
  constructor(
    private route: ActivatedRoute,
    private jobservice: JobsService,
    private handlerservice: GlobalerrorhandlerService
  ) {}

  ngOnInit(): void {
    this.getCategory();
    this.getCities();
    this.getStates();

    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.jobservice.getAllJobsById(parseInt(id)).subscribe({
            next: (response: apiresponse) => {
              if (response.message == '') {
                this.error = this.handlerservice.handleError(response.error);
              } else {
                this.newjob = response.data;
              }
            },
            error: (err: HttpErrorResponse) => {
              this.error = this.handlerservice.handleError(err);
            },
          });
        }
      },
    });
  }
  private getStatesFromCities(cityId: number) {
    if (cityId == 4) {
      this.newjob.stateid = this.stateList.find((x) => x.id == 2).id;
    } else if (cityId == 6) {
      this.newjob.stateid = this.stateList.find((x) => x.id == 4).id;
    } else {
      this.newjob.stateid = this.stateList.find((x) => x.id == cityId).id;
    }
  }

  editJob(form: NgForm) {
    if (form.valid) {
      this.getStatesFromCities(this.newjob.cityid);
      this.newjob.location =
        this.cityList.find((x) => x.id == this.newjob.cityid).name +
        ', ' +
        this.stateList.find((x) => x.id == this.newjob.stateid).name;
      this.jobservice.editJobs(this.newjob.id, this.newjob).subscribe({
        next: (response) => {
          alert(response.message);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        },
      });
    }
  }

  deleteJob(id: number) {
    this.jobservice.deleteJobs(id).subscribe({
      next: (response) => {
        alert(response.message);
      },
      error: (errResponse) => {
        alert(errResponse);
      },
    });
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
