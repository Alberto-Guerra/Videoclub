import * as MoviesActions from './movies.actions';

import { createReducer, on, Action } from '@ngrx/store';
import { initialState } from './movies.state';
import { ALL_CATEGORIES, ALL_STATES, ALL_TEXT_TO_FIND } from '../movie.model';

export const moviesReducers = createReducer(
  initialState,

  on(MoviesActions.LoadAllMoviesSuccess, (state, { movies }) => {
    return {
      ...state,
      movies: movies,
      filteredMovies: movies,
      moviesLoaded: true,
      error: undefined,
    };
  }),

  on(MoviesActions.LoadAllMoviesFail, (state, { error }) => {
    return {
      ...state,
      movies: [],
      filteredMovies: [],
      moviesLoaded: false,
      error: error,
    };
  }),

  on(MoviesActions.LoadCategoriesSuccess, (state, { categories }) => {
    return {
      ...state,
      categories: categories,
      error: undefined,
    };
  }),

  on(MoviesActions.LoadCategoriesFail, (state, { error }) => {
    alert(error);
    return {
      ...state,
      categories: [],
      error: error,
    };
  }),

  on(MoviesActions.LoadCategoriesSuccess, (state, { categories }) => {
    return {
      ...state,
      categories: categories,
      error: undefined,
    };
  }),

  on(MoviesActions.LoadCategoriesFail, (state, { error }) => {
    return {
      ...state,
      categories: [],
      error: error,
    };
  }),

  on(MoviesActions.FilterMovies, (state) => {
    let filteredMovies = state.movies;
    if (state.categoryToFilter !== ALL_CATEGORIES) {
      filteredMovies = filteredMovies.filter(
        (movie) => movie.category === state.categoryToFilter
      );
    }
    if (state.stateToFilter !== ALL_STATES) {
      filteredMovies = filteredMovies.filter(
        (movie) => movie.state === state.stateToFilter
      );
    }
    if (state.textToFind !== ALL_TEXT_TO_FIND) {
      filteredMovies = filteredMovies.filter((movie) =>
        movie.title.toLowerCase().includes(state.textToFind.toLowerCase())
      );
    }
    return {
      ...state,
      filteredMovies: filteredMovies,
    };
  }),

  on(MoviesActions.UpdateTextToFind, (state, { textToFind }) => {
    return {
      ...state,
      textToFind: textToFind,
    };
  }),

  on(MoviesActions.UpdateCategoryToFilter, (state, { categoryToFilter }) => {
    return {
      ...state,
      categoryToFilter: categoryToFilter,
    };
  }),

  on(MoviesActions.UpdateStateToFilter, (state, { stateToFilter }) => {
    return {
      ...state,
      stateToFilter: stateToFilter,
    };
  }),

  on(MoviesActions.SetSelectedMovie, (state, { selectedMovie }) => {
    return {
      ...state,
      selectedMovie: selectedMovie,
    };
  }),

  on(MoviesActions.logState, (state) => {
    console.log(state);
    return state;
  }),

  on(MoviesActions.RentMovieSuccess, (state, { movies }) => {
    return {
      ...state,
      movies: movies,
      filteredMovies: movies,
      error: undefined,
    };
  }),

  on(MoviesActions.RentMovieFail, (state, { error }) => {
    return {
      ...state,
      error: error,
    };
  }),

  on(MoviesActions.ReturnMovieSuccess, (state, { movies }) => {
    return {
      ...state,
      movies: movies,
      filteredMovies: movies,
      error: undefined,
    };
  }),

  on(MoviesActions.ReturnMovieFail, (state, { error }) => {
    return {
      ...state,
      error: error,
    };
  }),

  on(MoviesActions.toggleEditMode, (state) => {
    return {
      ...state,
      editMode: !state.editMode,
    };
  }),

  on(MoviesActions.UpdateMovieSuccess, (state, { movies }) => {
    return {
      ...state,
      movies: movies,
      filteredMovies: movies,
      error: undefined,
    };
  }),

  on(MoviesActions.UpdateMovieFail, (state, { error }) => {
    return {
      ...state,
      error: error,
    };
  }),

  on(MoviesActions.CreateMovieSuccess, (state, { movies }) => {
    return {
      ...state,
      movies: movies,
      filteredMovies: movies,
      error: undefined,
    };
  }),

  on(MoviesActions.CreateMovieFail, (state, { error }) => {
    return {
      ...state,
      error: error,
    };
  }),

  on(MoviesActions.DeleteMovieSuccess, (state, { movies }) => {
    return {
      ...state,
      movies: movies,
      filteredMovies: movies,
      error: undefined,
    };
  }),

  on(MoviesActions.DeleteMovieFail, (state, { error }) => {
    return {
      ...state,
      error: error,
    };
  })
);
