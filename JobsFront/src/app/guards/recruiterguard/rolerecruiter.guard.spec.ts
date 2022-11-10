import { TestBed } from '@angular/core/testing';

import { RolerecruiterGuard } from './rolerecruiter.guard';

describe('RolerecruiterGuard', () => {
  let guard: RolerecruiterGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(RolerecruiterGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
