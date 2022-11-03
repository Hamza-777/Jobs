import { TestBed } from '@angular/core/testing';

import { BlogsServiceService } from './blogs-service.service';

describe('BlogsServiceService', () => {
  let service: BlogsServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlogsServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
