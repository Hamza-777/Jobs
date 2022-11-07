import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GotoCourseComponent } from './goto-course.component';

describe('GotoCourseComponent', () => {
  let component: GotoCourseComponent;
  let fixture: ComponentFixture<GotoCourseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GotoCourseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GotoCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
