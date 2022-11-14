import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/user-components/login/login.component';
import { HomeComponent } from './components/misc-components/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard } from './guards/authguard/auth.guard';
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
import { CourseComponent } from './components/course-components/course/course.component';
import { AddCourseComponent } from './components/course-components/add-course/add-course.component';
import { EditCourseComponent } from './components/course-components/edit-course/edit-course.component';
import { ListoutCoursesComponent } from './components/course-components/listout-courses/listout-courses.component';
import { JobsComponent } from './components/job-components/jobs/jobs.component';
import { CreateJobsComponent } from './components/job-components/create-jobs/create-jobs.component';
import { EditJobsComponent } from './components/job-components/edit-jobs/edit-jobs.component';
import { NavbarComponent } from './components/misc-components/navbar/navbar.component';
import { JobComponent } from './components/job-components/job/job.component';
import { NotFoundComponent } from './components/misc-components/not-found/not-found.component';
import { ViewCourseComponent } from './components/course-components/view-course/view-course.component';
import { LoadingComponent } from './components/misc-components/loading/loading.component';
import { YouTubePlayerModule } from '@angular/youtube-player';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ServererrorComponent } from './components/misc-components/servererror/servererror.component';
import { NoresultComponent } from './components/misc-components/noresult/noresult.component';

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
    CourseComponent,
    AddCourseComponent,
    EditCourseComponent,
    ListoutCoursesComponent,
    JobsComponent,
    CreateJobsComponent,
    EditJobsComponent,
    NavbarComponent,
    JobComponent,
    NotFoundComponent,
    ViewCourseComponent,
    LoadingComponent,
    ServererrorComponent,
    NoresultComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    YouTubePlayerModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),

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
