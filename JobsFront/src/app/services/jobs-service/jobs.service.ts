import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Job } from '../../_interfaces/Job';
import { map, observable, Observable } from 'rxjs';
import { JobParams } from '../../_interfaces/jobParams';
import { City } from '../../_interfaces/City';
import { State } from '../../_interfaces/State';
import { Category } from '../../_interfaces/Category';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class JobsService {
  baseUrl = 'https://localhost:7067/api/Jobs';
  constructor(private http: HttpClient) {}
  getAllJobs() {
    return this.http
      .get<Job[]>(environment.ApiUrl + 'Jobs', { observe: 'response' })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getAllJobsById(id: number) {
    return this.http.get<Job>(environment.ApiUrl + 'Jobs/' + id);
  }

  getAllCity() {
    return this.http.get<City[]>(environment.ApiUrl + 'Jobs' + '/city');
  }

  getAllState() {
    return this.http.get<State[]>(environment.ApiUrl + 'Jobs' + '/state');
  }
  getAllCategory() {
    return this.http.get<Category[]>(environment.ApiUrl + 'Jobs' + '/category');
  }
  postJobs(postJobRequest: Job): Observable<Job> {
    return this.http.post<Job>(environment.ApiUrl + 'Jobs', postJobRequest);
  }
  editJobs(id: number, editedJobRequest: Job) {
    return this.http.put<Job>(
      environment.ApiUrl + 'Jobs/' + id,
      editedJobRequest
    );
  }

  deleteJobs(id: number) {
    return this.http.delete<Job>(environment.ApiUrl + 'Jobs/' + id);
  }
}
