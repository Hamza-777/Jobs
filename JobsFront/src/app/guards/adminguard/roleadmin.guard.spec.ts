import { TestBed } from '@angular/core/testing';

import { RoleadminGuard } from './roleadmin.guard';

describe('RoleadminGuard', () => {
  let guard: RoleadminGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(RoleadminGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
