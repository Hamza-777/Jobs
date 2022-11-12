import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GlobalerrorhandlerService {
  errormsg!: any;
  constructor() {}
  handleError(error_response: any): any {
    console.log(error_response);
    return error_response;
  }
}
