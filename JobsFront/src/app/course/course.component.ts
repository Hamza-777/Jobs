
import { Component, ElementRef, OnInit,ViewChild } from '@angular/core';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  @ViewChild('search',{static:true}) searchTerm:ElementRef;

  title = 'mdb-angular-ui-kit-free';

  items = ['Action', 'Another action', 'Something else here'];
  coursess=["Business Analysis",'Commercial Law','Human Resources','Accounts','Corporate','Tax Planning',"Machine Learning",'Web Development','Software Development'];
  courseCategories=['Technology','Business Management','Finance Management'];
  filteredItems = this.items;

  searchItems(event: any) {
    const value = event.target.value;

    this.filterItems(value);
  }

   

  filterItems(value: string) {
    const filterValue = value.toLowerCase();
    this.filteredItems = this.items.filter((item: string) =>
      item.toLowerCase().includes(filterValue)
    );
  }

  constructor() { }

  ngOnInit(): void {
  }

}


















// @Component({
//   selector: 'app-course',
//   templateUrl: './course.component.html',
//   styleUrls: ['./course.component.css']
// })
// export class CourseComponent implements OnInit {

//   constructor() { }

//   ngOnInit(): void {
//   }

// }



// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-course',
//   templateUrl:  './course.component.html',
//   styleUrls: ['./course.component.css']
// })
// export class AppComponent {
//   title = 'mdb-angular-ui-kit-free';

//   items = ['Action', 'Another action', 'Something else here'];
//   filteredItems = this.items;

//   searchItems(event: any) {
//     const value = event.target.value;

//     this.filterItems(value);
//   }

//   filterItems(value: string) {
//     const filterValue = value.toLowerCase();
//     this.filteredItems = this.items.filter((item: string) =>
//       item.toLowerCase().includes(filterValue)
//     );
//   }
// }