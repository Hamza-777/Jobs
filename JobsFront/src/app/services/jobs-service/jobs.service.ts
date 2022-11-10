import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Job } from '../../_interfaces/Job';
import { map, observable, Observable } from 'rxjs';
import { City } from '../../_interfaces/City';
import { State } from '../../_interfaces/State';
import { Category } from '../../_interfaces/Category';
import { environment } from 'src/environments/environment';
import { apiresponse } from 'src/app/_interfaces/apiresponse';

@Injectable({
  providedIn: 'root',
})
export class JobsService {
  baseUrl = 'https://localhost:7067/api/Jobs';
  constructor(private http: HttpClient) {}
  getAllJobs() {
    return this.http
      .get<apiresponse>(environment.ApiUrl + 'Jobs', { observe: 'response' })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getAllJobsById(id: number) {
    return this.http.get<apiresponse>(environment.ApiUrl + 'Jobs/' + id);
  }

  getAllCity() {
    return this.http.get<apiresponse>(environment.ApiUrl + 'Jobs' + '/city');
  }

  getAllState() {
    return this.http.get<apiresponse>(environment.ApiUrl + 'Jobs' + '/state');
  }
  getAllCategory() {
    return this.http.get<apiresponse>(environment.ApiUrl + 'Jobs' + '/category');
  }
  postJobs(postJobRequest: Job){
    return this.http.post<apiresponse>(environment.ApiUrl + 'Jobs', postJobRequest);
  }
  editJobs(id: number, editedJobRequest: Job) {
    return this.http.put<apiresponse>(
      environment.ApiUrl + 'Jobs/' + id,
      editedJobRequest
    );
  }

  deleteJobs(id: number) {
    return this.http.delete<apiresponse>(environment.ApiUrl + 'Jobs/' + id);
  }
}
