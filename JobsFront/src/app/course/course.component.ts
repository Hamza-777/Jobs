
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  title = 'mdb-angular-ui-kit-free';

  items = ['Action', 'Another action', 'Something else here'];
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