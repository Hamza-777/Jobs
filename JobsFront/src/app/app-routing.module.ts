import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { BlogsComponent } from './blogs/blogs.component';
import { CreateBlogComponent } from './create-blog/create-blog.component';
import { ViewBlogComponent } from './view-blog/view-blog.component';
import { JobsComponent } from './jobs/jobs.component';
import { JobsDetailsComponent } from './jobs-details/jobs-details.component';
const routes: Routes = [
{path:'login',component : LoginComponent},
{path:'',component : HomeComponent ,canActivate:[AuthGuard]},
{path:'register',component : RegisterComponent },
{path: 'createblogs', component: CreateBlogComponent},
{path: 'blogs',component: BlogsComponent},
{path: 'blog/:blogId',component: ViewBlogComponent},
{path: 'jobs',component: JobsComponent},
{path: 'jobs/:id',component: JobsDetailsComponent},

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
