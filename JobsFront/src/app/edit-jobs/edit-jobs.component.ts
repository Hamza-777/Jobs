import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JobsService } from '../services/jobs.service';
import { Job } from '../_interfaces/Job';

@Component({
  selector: 'app-edit-jobs',
  templateUrl: './edit-jobs.component.html',
  styleUrls: ['./edit-jobs.component.css']
})
export class EditJobsComponent implements OnInit {
  newjob:Job;
  constructor(private route:ActivatedRoute, private jobservice:JobsService) { }

  ngOnInit(): void {
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

}
