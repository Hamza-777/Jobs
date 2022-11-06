import { Component, OnInit } from '@angular/core';
import { JobsService } from '../services/jobs.service';
import { Category } from '../_interfaces/Category';
import { City } from '../_interfaces/City';
import { Job } from '../_interfaces/Job';
import { JobParams } from '../_interfaces/jobParams';
import { State } from '../_interfaces/State';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {
  jobsList: Job[];
  cityList: City[];
  stateList: State[];
  categoryList: Category[];
  cityIdSelected: number;
  stateIdSelected: number;
  categoryIdSelected: number;
  // jobparam!: JobParams;

  constructor(private jobservice: JobsService) { }

  ngOnInit(): void {
    this.getJobs();
    this.getCities();
    this.getCategory();
    this.getStates();
  }

  getJobs() {
    this.jobservice.getAllJobs(this.cityIdSelected,
      this.categoryIdSelected, this.stateIdSelected)
      .subscribe({
        next: (jobsdata) => {
          this.jobsList = jobsdata;
          console.log(jobsdata);
        },
        error: (errReponse) => {
          console.log(errReponse);
        }
      })
  }
  

  getCities() {
    this.jobservice.getAllCity().subscribe({
      next: (cityData) => {
        this.cityList = [{id:0, name:'All'}, ...this.removeObjectWithId(cityData, 5)];
        console.log("city");
        console.log(cityData);
      },
      error: (errReponse) => {
        console.log(errReponse);
      }
    })
  }
  getCategory() {
    this.jobservice.getAllCategory().subscribe({
      next: (categoryData) => {
        this.categoryList = [{id:0, name:'All'}, ...categoryData];
        console.log("category");
        console.log(categoryData);
      },
      error: (errReponse) => {
        console.log(errReponse);
      }
    })
  }
  getStates() {
    this.jobservice.getAllState().subscribe({
      next: (stateData) => {
        this.stateList = [{id:0, name:'All'}, ...this.removeObjectWithId(stateData, 5)];
        

      },
      error: (errReponse) => {
        console.log(errReponse);
      }
    })
  }

  onCitySelected(cityId: number) {
    this.cityIdSelected = cityId;
    this.getJobs();
  }
  onStateSelected(stateId: number) {
    this.stateIdSelected = stateId;
    this.getJobs();
  }
  onCategorySelected(categoryId: number) {
    this.categoryIdSelected = categoryId;
    this.getJobs();
  }

  // remove element from list 
  private removeObjectWithId(arr, id) {
    const objWithIdIndex = arr.findIndex((obj) => obj.id === id);
    arr.splice(objWithIdIndex, 1);
  
    return arr;
  }
}
