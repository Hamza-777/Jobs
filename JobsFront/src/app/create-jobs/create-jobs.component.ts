import { Component, OnInit } from '@angular/core';
import { JobsService } from '../services/jobs.service';
import { Job } from '../_interfaces/Job';
import { JobsComponent } from '../jobs/jobs.component';
import { Category } from '../_interfaces/Category';
import { City } from '../_interfaces/City';
import { State } from '../_interfaces/State';
import { NgForm } from '@angular/forms';
import { formatCurrency } from '@angular/common';

@Component({
  selector: 'app-create-jobs',
  templateUrl: './create-jobs.component.html',
  styleUrls: ['./create-jobs.component.css']
})
export class CreateJobsComponent implements OnInit {
 newjob: any;
 categoryList:Category[];
 cityList:City[];
 stateList:State[];
  constructor(private jobservice: JobsService) {
this.newjob={
  id:0,
  title:'',
  description:'',
  redirect_url:'',
  salary_max:0,
  salary_min:0,
  company:'',
  created:Date().toString(),
  category:{id:0, name:''},
  categoryid:0,
  state: {id:0, name:''},
  stateid:0,
  city: {id:0, name:''},
  cityid:0,
  userid:1
}

   }

  ngOnInit(): void {
    console.log(this.categoryList);
   this.getCities();
   this.getCategory();
   this.getStates();
   console.log(this.categoryList);

  }

createJobs(form:NgForm){
  if(form.valid)
  {
    this.newjob.categoryid=this.newjob.category.id;
    this.newjob.cityid=this.newjob.city.id;



    console.log(this.newjob.category.id);
    console.log(this.newjob.category.name);
    console.log(this.newjob.city.id);
    console.log(this.newjob.city.name);

    // set state from city entered
    // location state + city
  console.log("entered create job");

  this.jobservice.postJobs(this.newjob).subscribe({
    next:(response)=>{
      console.log(response);
    },
    error:(errResponse)=>{
      console.log(errResponse);
    }
    
  })
}
}

  getCities() {
    this.jobservice.getAllCity().subscribe({
      next: (cityData) => {
        this.cityList = this.removeObjectWithId(cityData, 5);
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
        this.categoryList = categoryData;
    console.log("got data successfully");

        // return this.categoryList;
        
      },
      error: (errReponse) => {
        console.log(errReponse);
      }
    })
  }
  getStates() {
    this.jobservice.getAllState().subscribe({
      next: (stateData) => {
        this.stateList = [this.removeObjectWithId(stateData, 5)];
        

      },
      error: (errReponse) => {
        console.log(errReponse);
      }
    })
  }

   // remove element from list 
   private removeObjectWithId(arr, id) {
    const objWithIdIndex = arr.findIndex((obj) => obj.id === id);
    arr.splice(objWithIdIndex, 1);
  
    return arr;
  }
}
