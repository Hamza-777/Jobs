import { NgModule } from '@angular/core';
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
import { JobsComponent } from './jobs/jobs.component';
import { JobsDetailsComponent } from './jobs-details/jobs-details.component';
import { CreateJobsComponent } from './create-jobs/create-jobs.component';
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
    JobsComponent,
    JobsDetailsComponent,
    CreateJobsComponent,
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
  providers:[AuthGuard],
})
export class AppModule { }
