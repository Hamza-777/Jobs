import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GlobalerrorhandlerService {
  errormsg!:any
  constructor() { }
  handleError(error_response: any): void 
  {
    console.log()
    console.log(error_response);
    // this.errormsg=error_response;
    // console.log(this.errormsg);
    // if(error_response.error.title!=null) //API Validation gives title
    //   this.errormsg = JSON.stringify(this.errormsg["errors"])
    //   this.errormsg = this.errormsg.replace(/[{""}]|/g,"")
    //   this.errormsg = this.errormsg.replace(",","<br>")
    //   console.error("An error occured:",this.errormsg);
    //   return this.errormsg;
  }
}
