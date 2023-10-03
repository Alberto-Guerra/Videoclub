import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as MoviesSelectors from '../store/movies.selectors';
import * as MoviesActions from '../store/movies.actions';
import { Movie } from '../movie.model';
import { Actions, ofType } from '@ngrx/effects';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css'],
})
export class AddMovieComponent {
  movie: Movie;
  categories: string[] = [];
  private unsubscribe$: Subject<void> = new Subject<void>();
  errorString: string = '';
  constructor(
    private router: Router,
    private store: Store<AppState>,
    private actions$: Actions
  ) {
    this.store
      .select(MoviesSelectors.editMode)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((editMode) => {
        if (!editMode) {
          this.router.navigate(['/movies']);
        }
      });

    //the categories are loaded from the store
    this.store.select(MoviesSelectors.categories).subscribe((categories) => {
      this.categories = categories;
    });

    this.movie = {
      title: '',
      description: '',
      category: this.categories[0],
      photoURL: '',
      state: 'Available',
    };
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  //action to create or update the movie is dispatched on sumbit
  onSubmit() {
    this.store.dispatch(
      MoviesActions.CreateMovie({ movie: { ...this.movie } })
    );

    //in case of error the error message is displayed
    this.actions$
      .pipe(ofType(MoviesActions.CreateMovieFail))
      .subscribe((action) => {
        this.errorString = action.error;
      });

    //in case of success, redirect to the movies page
    this.actions$
      .pipe(ofType(MoviesActions.CreateMovieSuccess))
      .subscribe(() => {
        this.router.navigate(['/movies']);
      });
  }

  //go back one step
  goBack() {
    this.router.navigate(['/movies']);
  }
}
