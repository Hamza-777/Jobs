import { Component, OnInit } from '@angular/core';
import { JobsService } from '../services/jobs.service';
import { Job } from '../_interfaces/Job';

@Component({
  selector: 'app-create-jobs',
  templateUrl: './create-jobs.component.html',
  styleUrls: ['./create-jobs.component.css']
})
export class CreateJobsComponent implements OnInit {
 newjob!: Job;

  constructor(private jobservice: JobsService) { }

  ngOnInit(): void {
    this.jobservice.postJobs(this.newjob).subscribe(
      {
        next:(data)=>
        {
          console.log(data);
        }
      }
    )
  }


}
