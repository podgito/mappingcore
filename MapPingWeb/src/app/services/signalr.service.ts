import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { Subject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SignalRConnectionInfo } from '../models/signal-rconnection-info';
import { Ping } from '../models/ping';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private readonly _http: HttpClient;
    // private readonly _baseUrl: string = "http://localhost:7071/api/"; //https://func-mappingdev.azurewebsites.net
    private readonly _baseUrl: string = "https://funcmappingdev.azurewebsites.net/api/"; //https://func-mappingdev.azurewebsites.net

    private hubConnection: HubConnection;
    events: Subject<Ping> = new Subject();

    constructor(http: HttpClient) {
        this._http = http;
    }

    private getConnectionInfo(): Observable<SignalRConnectionInfo> {
        let requestUrl = `${this._baseUrl}negotiate`;
        return this._http.get<SignalRConnectionInfo>(requestUrl);
    }

    init() {
        this.getConnectionInfo().subscribe(info => {
            let options = {
                accessTokenFactory: () => info.accessKey
            };

            this.hubConnection = new signalR.HubConnectionBuilder()
                .withUrl(info.endpoint, options)
                .configureLogging(signalR.LogLevel.Information)
                .build();

            this.hubConnection.start().catch(err => console.error(err.toString()));

            this.hubConnection.on('event', (data: Ping) => {
                this.events.next(data);
            });
        });
    }

    send(ping: Ping): Observable<void> { // this will be the contents of the library
        let requestUrl = `${this._baseUrl}message`;
        return this._http.post(requestUrl, ping).pipe(map((result: any) => { }));
    }
}
