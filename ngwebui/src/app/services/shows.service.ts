import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShowsService {
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/hubs/showshub') // SignalR hub URL
      .build();
  }

  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error: any) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  receiveServerName(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveServerName', (message: string) => {
        observer.next(message);
      });
    });
  }

  receiveMoviesList(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('ReceiveMoviesList', (message: string) => {
        observer.next(message);
      });
    });
  }

  receiveMoviesListNowPlaying(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveMoviesListNowPlaying', (message: string) => {
        observer.next(message);
      });
    });
  }

  receiveMoviesListPopular(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveMoviesListPopular', (message: string) => {
        observer.next(message);
      });
    });
  }

  receiveMoviesListTopRated(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveMoviesListTopRated', (message: string) => {
        observer.next(message);
      });
    });
  }

  receiveMoviesListUpcoming(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveMoviesListUpcoming', (message: string) => {
        observer.next(message);
      });
    });
  }

  getMoviesList(): void {
    this.hubConnection.invoke('GetMoviesList');
  }

  getShowsLists(): void {
    this.hubConnection.invoke('GetShowsLists');
  }
}
