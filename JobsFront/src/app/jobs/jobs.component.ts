<<<<<<< HEAD
import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';
import { JobsService } from '../services/jobs-service/jobs.service';
=======
import { Component, OnInit } from '@angular/core';
import { JobsService } from '../services/jobs.service';
>>>>>>> main
import { Category } from '../_interfaces/Category';
import { City } from '../_interfaces/City';
import { Job } from '../_interfaces/Job';
import { State } from '../_interfaces/State';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css'],
})
export class JobsComponent implements OnInit {
  jobsList: Job[];
  filteredJobs: Job[];
  cityList: City[];
  stateList: State[];
  categoryList: Category[];
<<<<<<< HEAD
  cityIdSelected: number;
  stateIdSelected: number;
  categoryIdSelected: number;
  search:string;
  sortSelected:string;
  error!:any;
  sortOptions=[
    {name: "Salary: low to high", value:"salaryAsc"},
    {name: "Salary: high to low", value:"salaryDsc"}
  ]
  // jobparam!: JobParams;

  constructor(private jobservice: JobsService,private handlerservice:GlobalerrorhandlerService) { }
=======
  cityIdSelected: number = 0;
  stateIdSelected: number = 0;
  categoryIdSelected: number = 0;
  searchQuery: string = '';
  sortOrder: string = 'default';

  constructor(private jobservice: JobsService) {}
>>>>>>> main

  ngOnInit(): void {
    this.getJobs();
    this.getCities();
    this.getCategory();
    this.getStates();
  }

  getJobs() {
<<<<<<< HEAD
    this.jobservice.getAllJobs(this.cityIdSelected,
      this.categoryIdSelected, this.stateIdSelected, this.sortSelected, this.search)
      .subscribe({
        next: (jobsdata) => {
          this.jobsList = jobsdata;
          console.log(jobsdata);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.handlerservice.handleError(err);
        }
      })
=======
    this.jobservice.getAllJobs().subscribe({
      next: (jobsdata) => {
        this.jobsList = jobsdata;
        this.filteredJobs = jobsdata;
      },
      error: (errReponse) => {
        console.log(errReponse);
      },
    });
>>>>>>> main
  }

  getCities() {
    this.jobservice.getAllCity().subscribe({
      next: (cityData) => {
        this.cityList = [
          { id: 0, name: 'All' },
          ...this.removeObjectWithId(cityData, 5),
        ];
      },
<<<<<<< HEAD
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    })
=======
      error: (errReponse) => {
        console.log(errReponse);
      },
    });
>>>>>>> main
  }

  getCategory() {
    this.jobservice.getAllCategory().subscribe({
      next: (categoryData) => {
        this.categoryList = [{ id: 0, name: 'All' }, ...categoryData];
      },
<<<<<<< HEAD
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    })
=======
      error: (errReponse) => {
        console.log(errReponse);
      },
    });
>>>>>>> main
  }

  getStates() {
    this.jobservice.getAllState().subscribe({
      next: (stateData) => {
        this.stateList = [
          { id: 0, name: 'All' },
          ...this.removeObjectWithId(stateData, 5),
        ];
      },
<<<<<<< HEAD
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      }
    })
=======
      error: (errReponse) => {
        console.log(errReponse);
      },
    });
>>>>>>> main
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
  }

  // remove element from list
  private removeObjectWithId(arr, id) {
    const objWithIdIndex = arr.findIndex((obj) => obj.id === id);
    arr.splice(objWithIdIndex, 1);

    return arr;
  }
}
