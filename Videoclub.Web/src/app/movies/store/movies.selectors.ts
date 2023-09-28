import { createFeatureSelector, createSelector } from "@ngrx/store";
import { MoviesState } from "./movies.state";

const selectMoviesState = createFeatureSelector<MoviesState>('movies');


export const allMovies = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.movies
);

export const filteredMovies = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.filteredMovies
);

export const categories = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.categories
);

export const selectedMovie = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.selectedMovie
);

export const moviesLoaded = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.moviesLoaded
);

export const stateToFilter = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.stateToFilter
);

export const categoryToFilter = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.categoryToFilter
);

export const textToFind = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.textToFind
);

export const movieById = (id : number) => createSelector( 
    selectMoviesState, 
    (state : MoviesState) => state.movies.find((movie) => movie.id === id));

export const editMode = createSelector(
    selectMoviesState,
    (state: MoviesState) => state.editMode
);
    
