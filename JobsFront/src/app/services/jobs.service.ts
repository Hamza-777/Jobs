import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Job } from '../_interfaces/Job';

@Injectable({
  providedIn: 'root'
})
export class JobsService {
baseUrl="https://localhost:7067/api/Jobs";
  constructor(private http:HttpClient) { }
getAllJobs(){
  return this.http.get<Job[]>(this.baseUrl);
}

}
