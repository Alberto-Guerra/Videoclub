import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as MoviesSelectors from '../store/movies.selectors';
import * as MoviesActions from '../store/movies.actions';
import { Movie } from '../movie.model';

@Component({
  selector: 'app-edit-movie',
  templateUrl: './edit-movie.component.html',
  styleUrls: ['./edit-movie.component.css'],
})
export class EditMovieComponent {
  movie: Movie | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private store: Store<AppState>
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    //if the id is not a number, redirect to the movies page
    if (isNaN(id)) {
      this.router.navigate(['/movies']);
    }

    //if the movies are not loaded, load them
    store.select(MoviesSelectors.moviesLoaded).subscribe((moviesLoaded) => {
      if (!moviesLoaded) {
        this.store.dispatch(MoviesActions.LoadAllMovies());
      }
    });

    // the movie to edit is selected from the store
    this.store.select(MoviesSelectors.movieById(id)).subscribe((movie) => {
      if (movie) {
        this.movie = { ...movie };
      }
    });
  }
}
