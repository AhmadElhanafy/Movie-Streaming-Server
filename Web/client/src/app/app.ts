import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('client');

  responseMessage: string = '';

  constructor(private http: HttpClient) {}

  callBackend() {
    this.http.get<{ message: string }>('http://backend/api/ping')
      .subscribe({
        next: (res) => this.responseMessage = res.message,
        error: () => this.responseMessage = 'Error contacting backend'
      });
  }
}
