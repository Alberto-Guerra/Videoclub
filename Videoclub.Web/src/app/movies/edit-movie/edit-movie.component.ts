import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as MoviesSelectors from '../store/movies.selectors';
import * as MoviesActions from '../store/movies.actions';
import { Movie } from '../movie.model';
import { Actions, ofType } from '@ngrx/effects';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-edit-movie',
  templateUrl: './edit-movie.component.html',
  styleUrls: ['./edit-movie.component.css'],
})
export class EditMovieComponent {
  movie: Movie | undefined;
  categories: string[] = [];
  private unsubscribe$: Subject<void> = new Subject<void>();
  errorString: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private store: Store<AppState>,
    private actions$: Actions
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

    //the categories are loaded from the store
    this.store.select(MoviesSelectors.categories).subscribe((categories) => {
      this.categories = categories;
    });

    this.store
      .select(MoviesSelectors.editMode)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((editMode) => {
        if (!editMode) {
          this.router.navigate(['/movies']);
        }
      });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  //action to create or update the movie is dispatched on sumbit
  onSubmit() {
    this.store.dispatch(
      MoviesActions.UpdateMovie({ movie: { ...this.movie! } })
    );

    //in case of error the error message is displayed
    this.actions$
      .pipe(ofType(MoviesActions.UpdateMovieFail))
      .subscribe((action) => {
        this.errorString = action.error;
      });

    //in case of success, redirect to the detailed movie page
    this.actions$
      .pipe(ofType(MoviesActions.UpdateMovieSuccess))
      .subscribe(() => {
        this.router.navigate(['/movies', this.movie!.id]);
      });
  }

  //go back one step
  goBack() {
    this.router.navigate(['/movies', this.movie!.id]);
  }
}
