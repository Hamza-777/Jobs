import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../../../services/admin-service/admin.service';
import { GlobalerrorhandlerService } from '../../../services/error-service/globalerrorhandler.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { apiresponse } from '../../../models/apiresponse';

@Component({
  selector: 'app-showusers',
  templateUrl: './showusers.component.html',
  styleUrls: ['./showusers.component.css'],
})
export class ShowusersComponent implements OnInit {
  users: any = null;
  filteredUsers: any = null;
  error: any;
  data: any;
  searchQuery: string = '';
  status: string = 'loading';

  constructor(
    private handlerservice: GlobalerrorhandlerService,
    private adminservice: AdminService,
    private notify: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getusers();
  }

  getusers() {
    this.adminservice.getusers().subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          console.log(response.error);
        } else {
          this.users = response.data;
          this.filteredUsers = response.data;

          if(this.filteredUsers) {
            this.status = 'loaded';
          } else {
            this.status = 'empty';
          }
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  deleteuser(id: number) {
    this.adminservice.deleteuser(id).subscribe({
      next: (response: apiresponse) => {
        if (response.message == '') {
          this.notify.showError(response.error);
        } else {
          this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => {
              this.router.navigate(['showusers']);
            });
          this.notify.showError(response.message);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.handlerservice.handleError(err);
      },
    });
  }

  filterUsers() {
      this.filteredUsers = this.users.filter((user) =>
        user.userName
          .toLocaleLowerCase()
          .includes(this.searchQuery.toLocaleLowerCase())
      );

    if(this.filteredUsers.length > 0) {
      this.status = 'loaded';
    } else {
      this.status = 'empty';
    }
  }
}
