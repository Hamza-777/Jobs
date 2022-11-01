import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { CourseComponent } from './course/course.component';
const routes: Routes = [
{path:'login',
component : LoginComponent},
{path:'course',
component : CourseComponent},
{path:'',
component : HomeComponent ,canActivate:[AuthGuard]},
{path:'register',
component : RegisterComponent }];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
