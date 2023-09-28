import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { Movie, RentHistory } from '../movie.model';
import * as MoviesSelectors from '../store/movies.selectors';
import * as MoviesActions from '../store/movies.actions';
import * as AuthSelectors from '../../auth/store/auth.selectors';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-detailed-movie',
  templateUrl: './detailed-movie.component.html',
  styleUrls: ['./detailed-movie.component.css']
})
export class DetailedMovieComponent {

  movie$? : Observable<Movie | undefined> 

  editMode = false;
  isAuthenticated = false;
  user_id : number = 0;

  constructor(private router : Router, private route : ActivatedRoute ,private store : Store<AppState>) {


    //if we didnt load the movies yet, we dispatch the action to load them
    this.store.select(MoviesSelectors.moviesLoaded).subscribe((moviesLoaded) => {
      if (!moviesLoaded) {
        this.store.dispatch(MoviesActions.LoadAllMovies()); 
      }
    });
    
    //we set the movie to display
    this.setMovie();

    //we subscribe to the store to get the edit mode

    this.store.select(MoviesSelectors.editMode).subscribe((editMode) => {
      this.editMode = editMode;
    });

    //we subscribe to the store to get the authentication status
    this.store.select(AuthSelectors.isAuthenticated).subscribe((isAuthenticated) => {
      this.isAuthenticated = isAuthenticated;
   });

   

  }

  //we get the url of the movie to display the image
  getUrl(movie:Movie){
    return movie.photoURL;
  }

  //we get the actual user id to rent the movie, then we dispatch the action to rent the movie 
  rentMovie(movie : Movie){
    let id : number;
    this.store.select(AuthSelectors.userId).subscribe( (user_id)  => {

      if(!user_id){
        return
      }

      id = Number.parseInt(user_id!)

      let historyToPass : RentHistory = {
        movie_id : movie.id!,
        user_id : id,
        rentDate : new Date().toISOString(),
        returnDate : null
      }

      this.store.dispatch(MoviesActions.RentMovie({history : historyToPass}));
      
      this.setMovie();
    })
    
  }

  //we get the id of the movie from the route, then we select the movie from the store
  setMovie(){
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.movie$ = this.store.select(MoviesSelectors.movieById(id))
  }

  //we dispatch the action to return the movie
  returnMovie(movie_id : number){
    this.store.dispatch(MoviesActions.ReturnMovie({movie_id : movie_id}));
    
  }

  //we navigate to the movies page
  goBack(){
    this.router.navigate(['/movies']);
  }

  //we navigate to the edit page of the movie
  editMovie(movie : Movie){ 
    this.router.navigate(['/movies/edit/'+ movie.id]);
  }

  //we dispatch the action to delete the movie, then we navigate to the movies page
  deleteMovie(movie : Movie){
    this.store.dispatch(MoviesActions.DeleteMovie({movie_id : movie.id!}));
    this.router.navigate(['/movies']);
  }

  
  //we check if the actual user is the one who rented the movie, if so we display "YOU" instead of the username
  getRentedText(movie : Movie) : string{
    let user : string;
    if(this.sameUser(movie)){
      user = "YOU";
    }
    else {
      user = movie.usernameRented + "";
    }

    return `Rented by ${user} on `;
  }


  //we check if the actual user is the one who rented the movie comparing the user id
  sameUser(movie : Movie): boolean{
    let user_id;
    this.store.select(AuthSelectors.userId).subscribe((userId) => {
      user_id = userId;
    });
    if(user_id){
      return movie.userid == parseInt(user_id) ;
    }
    return false;
    
  }


}
