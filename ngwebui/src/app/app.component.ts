import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms'
import { AppSignalRService } from './app-signalr.service';
import { MoviesService } from './services/movies.service'

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'ngwebui';
  receivedMessage: string = "";
  textBoxValue: string = '';

  constructor(private signalRService: AppSignalRService,
              private moviesService: MoviesService) { }

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveMessage().subscribe((message) => {
        console.log('Message Received: ' + message);
        this.receivedMessage = message;
      });
    });

    this.moviesService.startConnection().subscribe(() => {
      this.moviesService.receiveMessage().subscribe((message) => {
        console.log('Message Received from Movies: ' + message);
      });
    });
  }

  logTextBoxValue() {
    //console.log('Textbox value:', this.textBoxValue);
    this.sendMessage(this.textBoxValue);
  }

  sendMessage(message: string): void {
    this.signalRService.sendMessage(message);
  }

  getMovies(): void {
    this.moviesService.getMoviesList();
  }
}
