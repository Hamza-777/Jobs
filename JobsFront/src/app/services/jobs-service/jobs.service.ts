import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Job } from '../../models/Job';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { apiresponse } from 'src/app/models/apiresponse';

@Injectable({
  providedIn: 'root',
})
export class JobsService {
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
    return this.http.get<apiresponse>(
      environment.ApiUrl + 'Jobs' + '/category'
    );
  }
  postJobs(postJobRequest: Job) {
    return this.http.post<apiresponse>(
      environment.ApiUrl + 'Jobs',
      postJobRequest
    );
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
