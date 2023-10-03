import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Movie, RentHistory } from './movie.model';
import { environment } from 'src/enviroments/enviroment';
import { AuthService } from '../auth/auth.service';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class MoviesService {
  private moviesString = 'movies';

  constructor(private http: HttpClient, private service: AuthService) {}

  //calls the API asking for the movies
  getMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(environment.apiUrl + this.moviesString);
  }

  //calls the API asking for the categories
  getCategories(): Observable<string[]> {
    return this.http.get<string[]>(
      environment.apiUrl + this.moviesString + '/categories'
    );
  }

  //calls the API asking for renting the movie
  rentMovie(history: RentHistory): Observable<Movie[]> {
    return this.http
      .put(environment.apiUrl + this.moviesString + '/rent', history, {
        headers: this.service.getHeaders(),
      })
      .pipe(switchMap(() => this.getMovies()));
  }

  //calls the API asking for returning the movie with the id passed
  returnMovie(movie_id: number): Observable<Movie[]> {
    return this.http
      .post(
        environment.apiUrl + this.moviesString + `/return/${movie_id}`,
        {},
        { headers: this.service.getHeaders() }
      )
      .pipe(switchMap(() => this.getMovies()));
  }

  //calls the API asking for updating the movie passed
  updateMovie(movie: Movie): Observable<Movie[]> {
    return this.http
      .put(environment.apiUrl + this.moviesString + '/edit', movie, {
        headers: this.service.getHeaders(),
      })
      .pipe(switchMap(() => this.getMovies()));
  }

  //calls the API asking for creating a new movie
  createMovie(movie: Movie): Observable<Movie[]> {
    return this.http
      .post(environment.apiUrl + this.moviesString + '/add', movie, {
        headers: this.service.getHeaders(),
      })
      .pipe(switchMap(() => this.getMovies()));
  }

  //calls the API asking for deleting the movie with the id passed
  deleteMovie(movie_id: number): Observable<Movie[]> {
    return this.http
      .delete(environment.apiUrl + this.moviesString + `/delete/${movie_id}`, {
        headers: this.service.getHeaders(),
      })
      .pipe(switchMap(() => this.getMovies()));
  }
}
