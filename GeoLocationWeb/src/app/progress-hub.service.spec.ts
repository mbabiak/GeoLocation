import { TestBed } from '@angular/core/testing';

import { ProgressHubService } from './progress-hub.service';

describe('ProgressHubService', () => {
  let service: ProgressHubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProgressHubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
