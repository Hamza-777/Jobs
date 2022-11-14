import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/user-components/login/login.component';
import { HomeComponent } from './components/misc-components/home/home.component';
import { AuthGuard } from './guards/authguard/auth.guard';
import { RegisterComponent } from './components/user-components/register/register.component';
import { BlogsComponent } from './components/blog-components/blogs/blogs.component';
import { CreateBlogComponent } from './components/blog-components/create-blog/create-blog.component';
import { ViewBlogComponent } from './components/blog-components/view-blog/view-blog.component';
import { ForgotpwdComponent } from './components/user-components/forgotpwd/forgotpwd.component';
import { UpdateuserComponent } from './components/user-components/updateuser/updateuser.component';
import { ShowusersComponent } from './components/user-components/showusers/showusers.component';
import { ShowuserbyidComponent } from './components/user-components/showuserbyid/showuserbyid.component';
import { RegisteradminComponent } from './components/user-components/registeradmin/registeradmin.component';
import { AddCourseComponent } from './components/course-components/add-course/add-course.component';
import { EditCourseComponent } from './components/course-components//edit-course/edit-course.component';
import { ListoutCoursesComponent } from './components/course-components/listout-courses/listout-courses.component';
import { JobsComponent } from './components/job-components/jobs/jobs.component';
import { CreateJobsComponent } from './components/job-components/create-jobs/create-jobs.component';
import { EditJobsComponent } from './components/job-components/edit-jobs/edit-jobs.component';
import { NotFoundComponent } from './components/misc-components/not-found/not-found.component';
import { ViewCourseComponent } from './components/course-components/view-course/view-course.component';
import { RoleadminGuard } from './guards/adminguard/roleadmin.guard';
import { RolerecruiterGuard } from './guards/recruiterguard/rolerecruiter.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'createblogs',
    component: CreateBlogComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'blogs',
    component: BlogsComponent,
  },
  {
    path: 'blog/:blogId',
    component: ViewBlogComponent,
  },
  { path: 'forgotpwd', component: ForgotpwdComponent },
  {
    path: 'updateuser',
    component: UpdateuserComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'showusers',
    component: ShowusersComponent,
    canActivate: [RoleadminGuard],
  },
  {
    path: 'showuserbyid/:id',
    component: ShowuserbyidComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'registeradmin',
    component: RegisteradminComponent,
    canActivate: [RoleadminGuard],
  },
  {
    path: 'course/:courseId',
    component: ViewCourseComponent,
  },
  {
    path: 'add-course',
    component: AddCourseComponent,
    canActivate: [RoleadminGuard],
  },
  {
    path: 'edit-course/:courseId',
    component: EditCourseComponent,
    canActivate: [RoleadminGuard],
  },
  {
    path: 'courses',
    component: ListoutCoursesComponent,
  },

  { path: 'jobs', component: JobsComponent },
  { path: 'createjobs', component: CreateJobsComponent, canActivate: [RolerecruiterGuard] },
  {
    path: 'editjobs/:id',
    component: EditJobsComponent,
    canActivate: [RolerecruiterGuard],
  },
  {
    path: '**',
    component: NotFoundComponent,
    pathMatch: 'full',
  },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
