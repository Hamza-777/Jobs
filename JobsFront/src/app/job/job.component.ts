import { Component, OnInit, Input } from '@angular/core';
import { Job } from '../_interfaces/Job';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css'],
})
export class JobComponent implements OnInit {
  @Input() job!: Job;

  constructor() {}

  ngOnInit(): void {}

  applied() {
    alert('Applied Successfully');
  }
}
