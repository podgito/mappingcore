import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { Subject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SignalRConnectionInfo } from '@models/signal-rconnection-info';
import { Ping } from '@models/ping';
import { environment } from '@environments/environment';
import { DataRepositoryService } from '@services/data-repository.service';


@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private readonly _http: HttpClient;
    private readonly _baseUrl: string = environment.signalRServiceUrl;

    private hubConnection: HubConnection;
    events: Subject<Ping> = new Subject();

    constructor(http: HttpClient, private dataRepository: DataRepositoryService) {
        this._http = http;
    }

    private getConnectionInfo(): Observable<SignalRConnectionInfo> {
        let requestUrl = `${this._baseUrl}negotiate`;
        return this._http.get<SignalRConnectionInfo>(requestUrl);
    }

    init() {
        this.getConnectionInfo().subscribe(info => {
            let options = {
                accessTokenFactory: () => info.accessToken
            };

            this.hubConnection = new signalR.HubConnectionBuilder()
                .withUrl(info.url, options)
                .configureLogging(signalR.LogLevel.Information)
                .build();

            this.hubConnection.start().catch(err => console.error(err.toString()));

            this.hubConnection.on('event', (data: Ping) => {
                this.dataRepository.AddPing(data);
                this.events.next(data);
            });
        });
    }

    send(ping: Ping): Observable<void> { // this will be the contents of the library
        let requestUrl = `${this._baseUrl}message`;
        return this._http.post(requestUrl, ping).pipe(map((result: any) => { }));
    }
}
