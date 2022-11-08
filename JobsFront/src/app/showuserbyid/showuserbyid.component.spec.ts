import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowuserbyidComponent } from './showuserbyid.component';

describe('ShowuserbyidComponent', () => {
  let component: ShowuserbyidComponent;
  let fixture: ComponentFixture<ShowuserbyidComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowuserbyidComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowuserbyidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
