import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Job } from '../_interfaces/Job';
import { map, observable, Observable } from 'rxjs';
import { JobParams } from '../_interfaces/jobParams';
import { City } from '../_interfaces/City';
import { State } from '../_interfaces/State';
import { Category } from '../_interfaces/Category';

@Injectable({
  providedIn: 'root'
})
export class JobsService {
baseUrl="https://localhost:7067/api/Jobs";
  constructor(private http:HttpClient) { }
getAllJobs(cityId?:number, categoryId?:number, stateId?:number, sortName?:string, search?:string){
  let params= new HttpParams();
  if(categoryId){
    params= params.append('categoryId', categoryId.toString());
  }
  if(cityId){
    params= params.append('cityId', cityId.toString());
  }
  if(stateId){
    params= params.append('stateId', stateId.toString());
  }
  if(sortName != null){
    params= params.append('sort', sortName);

  }

  if(search != null){
    params=params.append('search', search);
  }

  return this.http.get<Job[]>(this.baseUrl, {observe: 'response', params}).pipe(
    map(response=>{
      return response.body;
    })
  )
}

getAllJobsById(id:number){
  return this.http.get<Job>(this.baseUrl+"/"+id);
}

getAllCity(){
  return this.http.get<City[]>(this.baseUrl+"/city");
}

getAllState(){
  return this.http.get<State[]>(this.baseUrl+"/state");
}
getAllCategory(){
  return this.http.get<Category[]>(this.baseUrl+"/category");
}
postJobs(postJobRequest:Job):Observable<Job>{
  return this.http.post<Job>(this.baseUrl,postJobRequest);
}
editJobs(id:number, editedJobRequest:Job){
  return this.http.put<Job>(this.baseUrl+id, editedJobRequest);
}

}
