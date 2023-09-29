import { Actions, createEffect, ofType } from '@ngrx/effects';
import { MoviesService } from '../movies.service';
import * as MoviesActions from './movies.actions';
import { Injectable } from '@angular/core';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class MoviesEffects {
  constructor(
    private actions$: Actions,
    private moviesService: MoviesService
  ) {}

  loadMovies$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MoviesActions.LoadAllMovies),
      mergeMap(() => {
        return this.moviesService.getMovies().pipe(
          map((movies) => MoviesActions.LoadAllMoviesSuccess({ movies })),
          catchError((error) =>
            of(MoviesActions.LoadAllMoviesFail({ error: error.error }))
          )
        );
      })
    )
  );

  loadCategories$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MoviesActions.LoadAllMoviesSuccess),
      mergeMap(() => {
        return this.moviesService.getCategories().pipe(
          map((categories) =>
            MoviesActions.LoadCategoriesSuccess({ categories })
          ),
          catchError((error) =>
            of(MoviesActions.LoadCategoriesFail({ error: error.error }))
          )
        );
      })
    )
  );

  //when one of the filters is updated, dispatch the action to filter the movies
  filterMovies$ = createEffect(() =>
    this.actions$.pipe(
      ofType(
        MoviesActions.UpdateTextToFind,
        MoviesActions.UpdateCategoryToFilter,
        MoviesActions.UpdateStateToFilter
      ),
      map(() => {
        return MoviesActions.FilterMovies();
      })
    )
  );

  rentMovie$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MoviesActions.RentMovie),
      mergeMap((action) => {
        return this.moviesService.rentMovie(action.history).pipe(
          map((movies) => MoviesActions.RentMovieSuccess({ movies })),
          catchError((error) =>
            of(MoviesActions.RentMovieFail({ error: error.error }))
          )
        );
      })
    )
  );

  returnMovie$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MoviesActions.ReturnMovie),
      mergeMap((action) => {
        return this.moviesService.returnMovie(action.movie_id).pipe(
          map((movies) => MoviesActions.ReturnMovieSuccess({ movies })),
          catchError((error) =>
            of(MoviesActions.ReturnMovieFail({ error: error.error }))
          )
        );
      })
    )
  );

  updateMovie$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MoviesActions.UpdateMovie),
      mergeMap((action) => {
        return this.moviesService.updateMovie(action.movie).pipe(
          map((movies) => MoviesActions.UpdateMovieSuccess({ movies })),
          catchError((error) =>
            of(MoviesActions.UpdateMovieFail({ error: error.error }))
          )
        );
      })
    )
  );

  createMovie$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MoviesActions.CreateMovie),
      mergeMap((action) => {
        return this.moviesService.createMovie(action.movie).pipe(
          map((movies) => MoviesActions.CreateMovieSuccess({ movies })),
          catchError((error) =>
            of(MoviesActions.CreateMovieFail({ error: error.error }))
          )
        );
      })
    )
  );

  deleteMovie$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MoviesActions.DeleteMovie),
      mergeMap((action) => {
        return this.moviesService.deleteMovie(action.movie_id).pipe(
          map((movies) => MoviesActions.DeleteMovieSuccess({ movies })),
          catchError((error) =>
            of(MoviesActions.DeleteMovieFail({ error: error.error }))
          )
        );
      })
    )
  );
}
