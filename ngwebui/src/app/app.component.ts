import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms'
import { ShowsService } from './services/shows.service'
import { environment } from './../environments/environment'

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'ngwebui';
  serverName: string = "";
  moviesList: any;
  moviesListNowPlaying: any;
  moviesListPopular: any;
  moviesListTopRated: any;
  moviesListUpcoming: any;
  receivedMessage: string = "";
  textBoxValue: string = '';

  constructor(private showsService: ShowsService) {}

  ngOnInit(): void {
    this.showsService.startConnection().subscribe(() => {

      this.showsService.receiveServerName().subscribe((message) => {
        this.serverName = message;
      });

      this.showsService.receiveMoviesList().subscribe((message) => {
        this.moviesList = JSON.parse(message);
        console.log(this.moviesList);
      });

      this.showsService.receiveMoviesListNowPlaying().subscribe((message) => {
        this.moviesListNowPlaying = JSON.parse(message);
      });

      this.showsService.receiveMoviesListPopular().subscribe((message) => {
        this.moviesListPopular = JSON.parse(message);
      });

      this.showsService.receiveMoviesListTopRated().subscribe((message) => {
        this.moviesListTopRated = JSON.parse(message);
      });

      this.showsService.receiveMoviesListUpcoming().subscribe((message) => {
        this.moviesListUpcoming = JSON.parse(message);
      });

    });
  }

  getMovies(): void {
    this.showsService.getMoviesList();
  }

  getShows(): void {

    this.moviesListNowPlaying = null;
    this.moviesListPopular = null;
    this.moviesListTopRated = null;
    this.moviesListUpcoming = null;

    this.showsService.getShowsLists();
  }
}
