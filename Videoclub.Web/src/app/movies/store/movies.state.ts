import {
  ALL_CATEGORIES,
  ALL_STATES,
  ALL_TEXT_TO_FIND,
  Movie,
} from '../movie.model';

export interface MoviesState {
  movies: Movie[];
  filteredMovies: Movie[];
  categories: string[];
  selectedMovie: Movie | null;
  categoryToFilter: string;
  stateToFilter: string;
  textToFind: string;
  moviesLoaded: boolean;
  editMode: boolean;
  error?: string;
}

export const initialState: MoviesState = {
  movies: [],
  filteredMovies: [],
  categories: [],
  selectedMovie: null,
  categoryToFilter: ALL_CATEGORIES,
  stateToFilter: ALL_STATES,
  textToFind: ALL_TEXT_TO_FIND,
  moviesLoaded: false,
  editMode: false,
  error: undefined,
};
