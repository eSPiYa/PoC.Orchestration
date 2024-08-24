import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms'
import { ShowsService } from './services/shows.service'

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'ngwebui';
  moviesList: any;
  receivedMessage: string = "";
  textBoxValue: string = '';

  constructor(private moviesService: ShowsService) { }

  ngOnInit(): void {
    this.moviesService.startConnection().subscribe(() => {
      this.moviesService.receiveMoviesList().subscribe((message) => {
        this.moviesList = JSON.parse(message);
        console.log(this.moviesList);
      });
    });
  }

  getMovies(): void {
    this.moviesService.getMoviesList();
  }
}
