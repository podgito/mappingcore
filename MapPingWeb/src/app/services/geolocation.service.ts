import { Injectable } from '@angular/core';
import { Html5GeoPosition } from '../models/html5-geo-position';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeolocationService {

  private geoLocationSource$ = new BehaviorSubject<Html5GeoPosition>(null);
  geoLocation$ = this.geoLocationSource$.asObservable();

  constructor() { }

  getLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.geoLocationSource$.next(position);
      });
    }
  }

}
