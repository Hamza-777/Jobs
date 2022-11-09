import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  adminregistration(user){
    return this.http.post<any>(environment.ApiUrl+"Admin/RegisterAdmin", user, {
      headers: new HttpHeaders({ "Content-Type": "application/json"})
    })
  }

  getuserbyid(id){
    return this.http.get<any>(environment.ApiUrl+"Admin/getuserbyid/"+id)
  }

  getusers(){
    return this.http.get<any>(environment.ApiUrl+"Admin/getusers")
  }

  deleteuser(id){
    return this.http.delete<any>(environment.ApiUrl+"Admin/deleteuser/"+id)
  }
}
