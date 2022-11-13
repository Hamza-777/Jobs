import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../../../services/admin-service/admin.service';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { apiresponse } from '../../../models/apiresponse';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-showuserbyid',
  templateUrl: './showuserbyid.component.html',
  styleUrls: ['./showuserbyid.component.css'],
})
export class ShowuserbyidComponent implements OnInit {
  id: number;
  user: any;
  error: any;
  currentUser: any = null;

  constructor(
    private activatedrouter: ActivatedRoute,
    private handlerservice: GlobalerrorhandlerService,
    private adminservice: AdminService
  ) {
    this.activatedrouter.paramMap.subscribe((params) => {
      this.id = Number(params.get('id'));
      console.log(this.id);
    });
  }

  ngOnInit(): void {
    this.currentUser = localStorage.getItem('jwt')
      ? jwt_decode(localStorage.getItem('jwt')!)
      : null;
    this.getuserbyid();
  }

  getuserbyid() {
    this.adminservice.getuserbyid(this.id).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.error = this.handlerservice.handleError(response.error);
        } else {
          this.user = response.data;
          console.log(response);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }
}
