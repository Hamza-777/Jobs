import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalerrorhandlerService } from '../services/error-service/globalerrorhandler.service';
import { JobsService } from '../services/jobs-service/jobs.service';
import { apiresponse } from '../_interfaces/apiresponse';
import { Job } from '../_interfaces/Job';

@Component({
  selector: 'app-jobs-details',
  templateUrl: './jobs-details.component.html',
  styleUrls: ['./jobs-details.component.css']
})
export class JobsDetailsComponent implements OnInit {
  jobDetails!: Job;
  error!: any;

  constructor(private route:ActivatedRoute, private jobservice: JobsService,private handlerservice:GlobalerrorhandlerService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id= params.get('id');
        if(id){
          this.jobservice.getAllJobsById(parseInt(id)).subscribe({
            next:(response:apiresponse)=>{
       
              if (response.message == "") {
                this.error = this.handlerservice.handleError(response.error);
              } else {
                this.jobDetails=response.data;
                console.log(response);
              }
            
            },
            error: (err: HttpErrorResponse) => {
              this.error = this.handlerservice.handleError(err);
            }
          })
        }

      }
    })
  }

}


