import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListoutCoursesComponent } from './listout-courses.component';

describe('ListoutCoursesComponent', () => {
  let component: ListoutCoursesComponent;
  let fixture: ComponentFixture<ListoutCoursesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListoutCoursesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListoutCoursesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
