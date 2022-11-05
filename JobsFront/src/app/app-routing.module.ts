import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { ForgotpwdComponent } from './forgotpwd/forgotpwd.component';
import { UpdateuserComponent } from './updateuser/updateuser.component';
import { ShowusersComponent } from './showusers/showusers.component';
import { ShowuserbyidComponent } from './showuserbyid/showuserbyid.component';
import { RegisteradminComponent } from './registeradmin/registeradmin.component';
const routes: Routes = [
{path:'login',
component : LoginComponent},
{path:'',
component : HomeComponent ,canActivate:[AuthGuard] },
{path:'register',
component : RegisterComponent },
{path:'forgotpwd',
component : ForgotpwdComponent },
{path:'updateuser',
component : UpdateuserComponent },
{path:'showusers',
component : ShowusersComponent },
{path:'showuserbyid/:id',
component : ShowuserbyidComponent },
{path:'registeradmin',
component : RegisteradminComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
