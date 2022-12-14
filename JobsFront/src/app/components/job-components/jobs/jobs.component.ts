import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { JobsService } from '../../../services/jobs-service/jobs.service';
import { apiresponse } from '../../../models/apiresponse';
import { Category } from '../../../models/Category';
import { City } from '../../../models/City';
import { Job } from '../../../models/Job';
import { State } from '../../../models/State';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css'],
})
export class JobsComponent implements OnInit {
  error: any;
  jobsList: Job[] = null;
  filteredJobs: Job[] = null;
  cityList: City[];
  stateList: State[];
  categoryList: Category[];
  cityIdSelected: number = 0;
  stateIdSelected: number = 0;
  categoryIdSelected: number = 0;
  searchQuery: string = '';
  sortOrder: string = 'default';
  status: string = 'loading';

  constructor(
    private jobservice: JobsService,
    private handlerservice: GlobalerrorhandlerService
  ) {}

  ngOnInit(): void {
    this.getJobs();
    this.getCities();
    this.getCategory();
    this.getStates();
  }

  getJobs() {
    this.jobservice.getAllJobs().subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          console.log(response.error);
        } else {
          this.jobsList = response.data;
          this.filteredJobs = response.data;

          if(this.filteredJobs) {
            this.status = 'loaded';
          } else {
            this.status = 'empty';
          }
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  getCities() {
    this.jobservice.getAllCity().subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          console.log(response.error);
        } else {
          this.cityList = [
            { id: 0, name: 'All' },
            ...this.removeObjectWithId(response.data, 5),
          ];
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
          console.log(response.error);
        } else {
          this.categoryList = [{ id: 0, name: 'All' }, ...response.data];
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
          console.log(response.error);
        } else {
          this.stateList = [
            { id: 0, name: 'All' },
            ...this.removeObjectWithId(response.data, 5),
          ];
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  filterJobs() {
    if (this.sortOrder === 'default') {
      this.filterJobsHelper();
    } else if (this.sortOrder === 'asc') {
      this.filterJobsHelper();
      this.filteredJobs.sort((a, b) => a.salary_max - b.salary_max);
    } else {
      this.filterJobsHelper();
      this.filteredJobs.sort((a, b) => b.salary_max - a.salary_max);
    }

    if(this.filteredJobs.length > 0) {
      this.status = 'loaded';
    } else {
      this.status = 'empty';
    }
  }

  filterJobsHelper() {
    this.filteredJobs = this.jobsList
      .filter((job) =>
        job.title
          .toLocaleLowerCase()
          .includes(this.searchQuery.toLocaleLowerCase())
      )
      .filter((job) =>
        this.categoryIdSelected == 0
          ? true
          : this.categoryIdSelected == job.categoryid
      )
      .filter((job) =>
        this.stateIdSelected == 0 ? true : this.stateIdSelected == job.stateid
      )
      .filter((job) =>
        this.cityIdSelected == 0 ? true : this.cityIdSelected == job.cityid
      );
  }

  resetFilters() {
    this.cityIdSelected = 0;
    this.stateIdSelected = 0;
    this.categoryIdSelected = 0;
    this.searchQuery = '';
    this.sortOrder = 'default';
    this.filteredJobs = this.jobsList;
  }

  private removeObjectWithId(arr, id) {
    const objWithIdIndex = arr.findIndex((obj) => obj.id === id);
    arr.splice(objWithIdIndex, 1);

    return arr;
  }
}
