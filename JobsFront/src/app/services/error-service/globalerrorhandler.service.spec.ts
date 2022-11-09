import { TestBed } from '@angular/core/testing';

import { GlobalerrorhandlerService } from './globalerrorhandler.service';

describe('GlobalerrorhandlerService', () => {
  let service: GlobalerrorhandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalerrorhandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
