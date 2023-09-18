import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { from, Observable, Subject } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProgressHubService {
  private hubConnection: signalR.HubConnection;
  private coordinatesResult$: Subject<number> = new Subject<number>();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/progressHub`)
      .build();

    this.hubConnection.on('CountUpdate', (data) => {
        this.coordinatesResult$.next(data);
    });
  }

  public getCoordinatesResult(): Observable<number>{
    return this.coordinatesResult$.asObservable();
  }

  public startConnection(): Promise<void> {
    return this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public getConnectionId(): Observable<string> {
    return from(this.hubConnection.invoke('GetConnectionId'))
  }
  
  ngOnDestroy(){
    this.hubConnection.stop();
  }

}
