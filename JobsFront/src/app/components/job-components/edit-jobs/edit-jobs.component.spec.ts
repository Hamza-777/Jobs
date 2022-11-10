import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditJobsComponent } from './edit-jobs.component';

describe('EditJobsComponent', () => {
  let component: EditJobsComponent;
  let fixture: ComponentFixture<EditJobsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditJobsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditJobsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
