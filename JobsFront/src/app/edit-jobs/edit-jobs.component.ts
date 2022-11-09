import { formatCurrency } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { JobsService } from '../services/jobs.service';
import { Category } from '../_interfaces/Category';
import { City } from '../_interfaces/City';
import { Job } from '../_interfaces/Job';
import { State } from '../_interfaces/State';

@Component({
  selector: 'app-edit-jobs',
  templateUrl: './edit-jobs.component.html',
  styleUrls: ['./edit-jobs.component.css']
})
export class EditJobsComponent implements OnInit {
  newjob:Job;
  categoryList:Category[];
  cityList:City[];
  stateList:State[];

  constructor(private route:ActivatedRoute, private jobservice:JobsService) { }

  ngOnInit(): void {
    
this.getCategory();
this.getCities();
this.getStates();

this.route.paramMap.subscribe({
  next:(params)=>{
    const id = params.get('id');
    if(id){
      this.jobservice.getAllJobsById(parseInt(id)).subscribe({
        next:(response)=>{
          this.newjob=response;
          console.log(response);
        },
        error:(errResponse)=>{
          console.log(errResponse);
        }
      })
    }
  }
})

  }
  private getStatesFromCities(cityId:number){
    // If pune map to maharastra
    if(cityId==4){
      this.newjob.stateid= this.stateList.find(x=>x.id==2).id;
    }
    // If delhi map to delhi
    else if( cityId == 6){
      this.newjob.stateid = this.stateList.find(x=>x.id == 4).id;
    }
    else{
      this.newjob.stateid= this.stateList.find(x=>x.id== cityId).id;
    }
  }


  editJob(form: NgForm)
  {
if(form.valid){

  this.getStatesFromCities(this.newjob.cityid);
    this.newjob.location = this.cityList.find(x=>x.id == this.newjob.cityid).name + ", "+ 
    this.stateList.find(x=>x.id == this.newjob.stateid).name;
    this.jobservice.editJobs(this.newjob.id, this.newjob).subscribe({
      next:(response)=>{
        console.log(response);
      },
      error:(errResponse)=>{
        console.log(errResponse);
      }
    })
}
  }

  deleteJob(id:number){
    
    this.jobservice.deleteJobs(id).subscribe({
      next:(response)=>{
        console.log(response);
      },
      error:(errResponse)=>{
        console.log(errResponse);
      }
    })
    // navigate to jobs page 
  }
 


  getCities() {
    this.jobservice.getAllCity().subscribe({
      next: (cityData) => {
        this.cityList = this.removeObjectWithId(cityData, 5);
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

      },
      error: (errReponse) => {
        console.log(errReponse);
      }
    })
  }
  getStates() {
    this.jobservice.getAllState().subscribe({
      next: (stateData) => {
        this.stateList = this.removeObjectWithId(stateData, 5);
        

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
