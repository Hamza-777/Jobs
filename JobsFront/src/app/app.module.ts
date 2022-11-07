import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { JwtModule } from "@auth0/angular-jwt";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import {HttpClientModule} from '@angular/common/http';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { BlogComponent } from './blog/blog.component';
import { BlogsComponent } from './blogs/blogs.component';
import { CreateBlogComponent } from './create-blog/create-blog.component';
import { ViewBlogComponent } from './view-blog/view-blog.component';
import { MarkdownPipe } from './pipes/markdown/markdown.pipe';
import { ForgotpwdComponent } from './forgotpwd/forgotpwd.component';
import { GlobalerrorhandlerService } from './globalerrorhandler.service';
import { UpdateuserComponent } from './updateuser/updateuser.component';
import { ShowusersComponent } from './showusers/showusers.component';
import { ShowuserbyidComponent } from './showuserbyid/showuserbyid.component';
import { RegisteradminComponent } from './registeradmin/registeradmin.component';
import { CourseComponent } from './course/course.component';
import { AddCourseComponent } from './course-crud/add-course/add-course.component';
import { CourseListComponent } from './course-crud/course-list/course-list.component';
import { EditCourseComponent } from './course-crud/edit-course/edit-course.component';
import { GotoCourseComponent } from './course-user/goto-course/goto-course.component';
import { ListoutCoursesComponent } from './course-user/listout-courses/listout-courses.component';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    
    
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7067"],
        disallowedRoutes: []
        
      }
    })
  ],
  bootstrap: [AppComponent],
  providers:[
  AuthGuard,
  {provide:ErrorHandler,useClass: GlobalerrorhandlerService},
  ],
})
export class AppModule { }
