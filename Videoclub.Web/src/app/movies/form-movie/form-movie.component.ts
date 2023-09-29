import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AppState } from 'src/app/store/app.state';
import { Movie } from '../movie.model';
import * as MoviesSelectors from '../store/movies.selectors';
import * as MoviesActions from '../store/movies.actions';
import { Actions, ofType } from '@ngrx/effects';

@Component({
  selector: 'app-form-movie',
  templateUrl: './form-movie.component.html',
  styleUrls: ['./form-movie.component.css'],
})
export class FormMovieComponent {
  @Input() movieInput: Movie | undefined;
  @Input() newMovie: boolean = false;

  movie: Movie;

  categories: string[] = [];

  private unsubscribe$: Subject<void> = new Subject<void>();
  errorString: string = '';

  constructor(
    private store: Store<AppState>,
    private router: Router,
    private actions$: Actions
  ) {
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

    //in the case the component is created without the edit mode on, redirects to the movies page
    this.store
      .select(MoviesSelectors.editMode)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((editMode) => {
        if (!editMode) {
          this.router.navigate(['/movies']);
        }
      });
  }

  ngOnInit(): void {
    //if the form is editing a movie, we copy the movie passed to the component
    if (!this.newMovie) {
      this.movie = { ...this.movieInput! };
    }
  }

  //the component unsubscribes to the store when is destroyed
  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  //action to create or update the movie is dispatched on sumbit
  onSubmit() {
    if (this.newMovie) {
      this.store.dispatch(
        MoviesActions.CreateMovie({ movie: { ...this.movie } })
      );
    } else {
      this.store.dispatch(
        MoviesActions.UpdateMovie({ movie: { ...this.movie } })
      );
    }

    //in case of error the error message is displayed
    this.actions$
      .pipe(
        ofType(MoviesActions.CreateMovieFail, MoviesActions.UpdateMovieFail)
      )
      .subscribe((action) => {
        this.errorString = action.error;
      });

    //in case of success, redirect to the movies page
    this.actions$
      .pipe(ofType(MoviesActions.CreateMovieSuccess))
      .subscribe(() => {
        this.router.navigate(['/movies']);
      });
    //or the detailed movie page
    this.actions$
      .pipe(ofType(MoviesActions.UpdateMovieSuccess))
      .subscribe(() => {
        this.router.navigate(['/movies', this.movieInput!.id]);
      });
  }

  //go back one step
  goBack() {
    if (this.newMovie) {
      this.router.navigate(['/movies']); //to the movies page in case of new movie
    } else {
      this.router.navigate(['/movies', this.movieInput!.id]); //to the detailed movie page in case of edit movie
    }
  }
}
