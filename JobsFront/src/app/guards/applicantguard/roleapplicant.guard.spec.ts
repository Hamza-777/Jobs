import { TestBed } from '@angular/core/testing';

import { RoleapplicantGuard } from './roleapplicant.guard';

describe('RoleapplicantGuard', () => {
  let guard: RoleapplicantGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(RoleapplicantGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
