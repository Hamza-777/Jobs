import { Component, OnInit } from '@angular/core';
import { JobsService } from '../services/jobs.service';
import { Job } from '../_interfaces/Job';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {
jobsList:Job[]=[];
  constructor(private jobservice: JobsService) { }

  ngOnInit(): void {
    this.jobservice.getAllJobs().subscribe((data)=>{
      this.jobsList=data;
        console.log(this.jobsList);
    }
    )
  }

}
