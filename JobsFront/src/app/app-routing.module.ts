import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { CourseComponent } from './course/course.component';
import { AddCourseComponent } from './course-crud/add-course/add-course.component';
import { EditCourseComponent } from './course-crud/edit-course/edit-course.component';
import { CourseListComponent } from './course-crud/course-list/course-list.component';
import { GotoCourseComponent } from './course-user/goto-course/goto-course.component';
import { ListoutCoursesComponent } from './course-user/listout-courses/listout-courses.component';

const routes: Routes = [
{
  path:'login',
  component : LoginComponent
},
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
  path:'course-user/listout-courses',
  component:ListoutCoursesComponent
},
{
  path:'',
  component : HomeComponent ,canActivate:[AuthGuard]
},
{
  path:'register',
  component : RegisterComponent
}];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
