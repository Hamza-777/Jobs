import { Component, Input, OnInit } from '@angular/core';
import { Category } from '../_interfaces/Category';
import { City } from '../_interfaces/City';
import { State } from '../_interfaces/State';

@Component({
  selector: 'app-drop-down',
  templateUrl: './drop-down.component.html',
  styleUrls: ['./drop-down.component.css']
})
export class DropDownComponent implements OnInit {
@Input() anylist: any[];

  constructor() { }

  ngOnInit(): void {
  }

}
