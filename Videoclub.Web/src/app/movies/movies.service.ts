import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Movie, RentHistory } from './movie.model';
import { environment } from 'src/enviroments/enviroment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  private moviesString = "movies";
 
  //this whole services just calls the api passing the token in the headers in the case of the methods that need it
  constructor(private http : HttpClient, private service : AuthService)  { }
  
  getMovies() : Observable<Movie[]> {
    return this.http.get<Movie[]>(environment.apiUrl + this.moviesString);
  }

  getCategories() : Observable<string[]> {
    return this.http.get<string[]>(environment.apiUrl + this.moviesString + "/categories");
  }

  rentMovie(history: RentHistory): Observable<Movie[]> {
    return this.http.put<Movie[]>(environment.apiUrl + this.moviesString + "/rent", history, { headers: this.service.getHeaders() });
  }

  returnMovie(movie_id : number) : Observable<Movie[]> {
    return this.http.post<Movie[]>(environment.apiUrl + this.moviesString + `/return/${movie_id}`,{},{ headers: this.service.getHeaders() });
  }

  updateMovie(movie : Movie) : Observable<Movie[]> {
    return this.http.put<Movie[]>(environment.apiUrl + this.moviesString + "/edit", movie, { headers: this.service.getHeaders() });  
  }

  createMovie(movie : Movie) : Observable<Movie[]> {
    return this.http.post<Movie[]>(environment.apiUrl + this.moviesString + "/add", movie, { headers: this.service.getHeaders() });
  }

  deleteMovie(movie_id : number) : Observable<Movie[]> {
    return this.http.delete<Movie[]>(environment.apiUrl + this.moviesString + `/delete/${movie_id}`, { headers: this.service.getHeaders() });
  }


  
  
}
