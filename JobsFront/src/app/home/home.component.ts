import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  isUserAuthenticated = (): boolean => {
    const token: string =localStorage.getItem("jwt")!;
    if(token){
    return true;
    }
    else{
    return false;
    }
  }

  logOut = () => {
    localStorage.removeItem("jwt");
  }
}
