import { TestBed, inject } from '@angular/core/testing';

import { D3SvgService } from './d3-svg.service';

describe('D3SvgService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [D3SvgService]
    });
  });

  it('should be created', inject([D3SvgService], (service: D3SvgService) => {
    expect(service).toBeTruthy();
  }));
});
