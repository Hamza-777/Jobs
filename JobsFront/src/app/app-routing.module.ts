import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { BlogsComponent } from './blogs/blogs.component';
import { CreateBlogComponent } from './create-blog/create-blog.component';
import { ViewBlogComponent } from './view-blog/view-blog.component';
import { ForgotpwdComponent } from './forgotpwd/forgotpwd.component';
import { UpdateuserComponent } from './updateuser/updateuser.component';
import { ShowusersComponent } from './showusers/showusers.component';
import { ShowuserbyidComponent } from './showuserbyid/showuserbyid.component';
import { RegisteradminComponent } from './registeradmin/registeradmin.component';
import { CourseComponent } from './course/course.component';
import { AddCourseComponent } from './course-crud/add-course/add-course.component';
import { EditCourseComponent } from './course-crud/edit-course/edit-course.component';
import { CourseListComponent } from './course-crud/course-list/course-list.component';
import { GotoCourseComponent } from './course-user/goto-course/goto-course.component';
import { ListoutCoursesComponent } from './course-user/listout-courses/listout-courses.component';

const routes: Routes = [
{path:'login',
component : LoginComponent},
{path:'',
component : HomeComponent ,canActivate:[AuthGuard] },
{path:'register',
component : RegisterComponent },
{
  path: 'createblogs',
  component: CreateBlogComponent ,canActivate:[AuthGuard]
},
{
  path: 'blogs',
  component: BlogsComponent
},
{
  path: 'blog/:blogId',
  component: ViewBlogComponent
},
{path:'forgotpwd',
component : ForgotpwdComponent },
{path:'updateuser',
component : UpdateuserComponent },
{path:'showusers',
component : ShowusersComponent },
{path:'showuserbyid/:id',
component : ShowuserbyidComponent },
{path:'registeradmin',
component : RegisteradminComponent },
{
  path:'course',
  component : CourseComponent
},
{
  path:'course-crud',
  component:CourseListComponent
},
{
  path:'course-crud/add',
  component:AddCourseComponent
},
{
  path:'course-crud/edit/:courseId',
  component:EditCourseComponent
},
{
  path:'course-user/goto-course/:courseName',
  component:GotoCourseComponent
},
{
  path:'course-user/listout-courses/:courseCategory',
  component:ListoutCoursesComponent
}];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
