import { TestBed, inject } from '@angular/core/testing';

import { DataRepositoryService } from './data-repository.service';

describe('DataRepositoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DataRepositoryService]
    });
  });

  it('should be created', inject([DataRepositoryService], (service: DataRepositoryService) => {
    expect(service).toBeTruthy();
  }));
});
