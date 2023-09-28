import { Component } from '@angular/core';
import { Movie } from '../movie.model';
import { AppState } from 'src/app/store/app.state';
import { Store } from '@ngrx/store';
import * as MoviesActions from '../store/movies.actions';
import * as MoviesSelectors from '../store/movies.selectors';
import { Router } from '@angular/router';





@Component({
  selector: 'app-all-movies',
  templateUrl: './all-movies.component.html',
  styleUrls: ['./all-movies.component.css']
})
export class AllMoviesComponent {

  Movies : Movie[] = [];
  editMode = false;

  categories : string[] = [];

  textToFind : string = "";
  categoryToFilter : string = "All";
  stateToFilter : string = "All";

  constructor(private store : Store<AppState>, private router : Router) {

    //we subscribe to the store to get the actual values of the fields in the filter

    store.select(MoviesSelectors.textToFind).subscribe((textToFind) => {
      this.textToFind = textToFind;
    });

    store.select(MoviesSelectors.categoryToFilter).subscribe((categoryToFilter) => {
      this.categoryToFilter = categoryToFilter;
    });

    store.select(MoviesSelectors.stateToFilter).subscribe((stateToFilter) => {
      this.stateToFilter = stateToFilter;
    });

    
    //then if we didnt load the movies yet, we dispatch the action to load them
    

    this.store.select(MoviesSelectors.moviesLoaded).subscribe((moviesLoaded) => {
      if (!moviesLoaded) {
        this.store.dispatch(MoviesActions.LoadAllMovies()); 
      }
    });

    // then we subscribe to the store to get the categories

    store.select(MoviesSelectors.categories).subscribe((categories) => {
      this.categories = categories;
    });

    // then we filter the movies and subscribe to the store to get them
    
    store.dispatch(MoviesActions.FilterMovies());
    store.select(MoviesSelectors.filteredMovies).subscribe((movies) => {
      this.Movies = movies;
    });

    // then we subscribe to the store to get the edit mode

    this.store.select(MoviesSelectors.editMode).subscribe((editMode) => {
      this.editMode = editMode;
    });
  }

  // when the state filter changes, we dispatch the action to update the state filter
  updateState(){
    this.store.dispatch(MoviesActions.UpdateStateToFilter({stateToFilter : this.stateToFilter}));
  }

  // when the category filter changes, we dispatch the action to update the category filter
  updateCategory(){
    this.store.dispatch(MoviesActions.UpdateCategoryToFilter({categoryToFilter : this.categoryToFilter}));
  }

  // when the text to find changes, we dispatch the action to update the text to find
  updateText(){
    this.store.dispatch(MoviesActions.UpdateTextToFind({textToFind : this.textToFind}));
  }






  

}
