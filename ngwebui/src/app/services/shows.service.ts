import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { } from './../../environments/environment'
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ShowsService {
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.baseUrl}${environment.hubsPath}/showshub`) // SignalR hub URL
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

  receiveTVListAiringToday(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveTVListAiringToday', (message: string) => {
        observer.next(message);
      });
    });
  }

  receiveTVListOnTheAir(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveTVListOnTheAir', (message: string) => {
        observer.next(message);        
      });
    });
  }

  receiveTVListPopular(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveTVListPopular', (message: string) => {
        observer.next(message);
      });
    });
  }

  receiveTVListTopRated(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('receiveTVListTopRated', (message: string) => {
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
