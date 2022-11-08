import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JobsService } from '../services/jobs.service';
import { Job } from '../_interfaces/Job';

@Component({
  selector: 'app-jobs-details',
  templateUrl: './jobs-details.component.html',
  styleUrls: ['./jobs-details.component.css']
})
export class JobsDetailsComponent implements OnInit {
  jobDetails!: Job;
  error!: any;

  constructor(private route:ActivatedRoute, private jobservice: JobsService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id= params.get('id');
        if(id){
          this.jobservice.getAllJobsById(parseInt(id)).subscribe({
            next:(response)=>{
              this.jobDetails=response
            },
            error: (err: HttpErrorResponse) => {
              console.log(err) ;
              if(err.error.title!=null)
                this.error=err.error.title;
              else
                this.error = err.error;
              }
          })
        }

      }
    })
  }

}


