import { Injectable } from '@angular/core';
import { Ping } from '@models/ping';

@Injectable({
  providedIn: 'root'
})
export class DataRepositoryService {

  constructor() { }


  public AddPing(ping: Ping) {
    localStorage.setItem(ping.region, JSON.stringify(ping));
  }
}
