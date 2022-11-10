import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/user-components/login/login.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './components/user-components/register/register.component';
import { BlogComponent } from './components/blog-components/blog/blog.component';
import { BlogsComponent } from './components/blog-components/blogs/blogs.component';
import { CreateBlogComponent } from './components/blog-components/create-blog/create-blog.component';
import { ViewBlogComponent } from './components/blog-components/view-blog/view-blog.component';
import { MarkdownPipe } from './pipes/markdown/markdown.pipe';
import { ForgotpwdComponent } from './components/user-components/forgotpwd/forgotpwd.component';
import { GlobalerrorhandlerService } from './services/error-service/globalerrorhandler.service';
import { UpdateuserComponent } from './components/user-components/updateuser/updateuser.component';
import { ShowusersComponent } from './components/user-components/showusers/showusers.component';
import { ShowuserbyidComponent } from './components/user-components/showuserbyid/showuserbyid.component';
import { RegisteradminComponent } from './components/user-components/registeradmin/registeradmin.component';
import { CourseComponent } from './components/course-components/course/course.component';
import { AddCourseComponent } from './components/course-components/add-course/add-course.component';
import { CourseListComponent } from './components/course-components/course-list/course-list.component';
import { EditCourseComponent } from './components/course-components/edit-course/edit-course.component';
import { GotoCourseComponent } from './components/course-components/goto-course/goto-course.component';
import { ListoutCoursesComponent } from './components/course-components/listout-courses/listout-courses.component';
import { JobsComponent } from './components/job-components/jobs/jobs.component';
import { CreateJobsComponent } from './components/job-components/create-jobs/create-jobs.component';
import { EditJobsComponent } from './components/job-components/edit-jobs/edit-jobs.component';
import { NavbarComponent } from './navbar/navbar.component';
import { JobComponent } from './components/job-components/job/job.component';
import { NotFoundComponent } from './not-found/not-found.component';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    BlogComponent,
    BlogsComponent,
    CreateBlogComponent,
    ViewBlogComponent,
    MarkdownPipe,
    ForgotpwdComponent,
    UpdateuserComponent,
    ShowusersComponent,
    ShowuserbyidComponent,
    RegisteradminComponent,
    CourseComponent,
    AddCourseComponent,
    CourseListComponent,
    EditCourseComponent,
    GotoCourseComponent,
    ListoutCoursesComponent,
    JobsComponent,
    CreateJobsComponent,
    EditJobsComponent,
    NavbarComponent,
    JobComponent,
    NotFoundComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,

    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:7067'],
        disallowedRoutes: [],
      },
    }),
  ],
  bootstrap: [AppComponent],
  providers: [
    AuthGuard,
    { provide: ErrorHandler, useClass: GlobalerrorhandlerService },
  ],
})
export class AppModule {}
