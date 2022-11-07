import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GlobalerrorhandlerService {
  errormsg!:any
  constructor() { }
  handleError(error_response: HttpErrorResponse): void 
  {
    this.errormsg=error_response.error;
    if(error_response.error.title!=null) //API Validation gives title
      this.errormsg = JSON.stringify(this.errormsg["errors"])
      this.errormsg = this.errormsg.replace(/[{""}]|/g,"")
      this.errormsg = this.errormsg.replace(",","<br>")
      console.error("An error occured:",this.errormsg);
      return this.errormsg;
  }
}
