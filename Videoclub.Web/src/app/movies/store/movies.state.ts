import { Movie } from "../movie.model";

export interface MoviesState {
    movies: Movie[];
    filteredMovies: Movie[];
    categories: string[];
    selectedMovie: Movie | null;
    categoryToFilter : string;
    stateToFilter : string;
    textToFind: string;
    moviesLoaded: boolean;
    editMode: boolean;
    error? : string;


    
}

export const initialState: MoviesState = {
    movies: [],
    filteredMovies: [],
    categories: [],
    selectedMovie: null,
    categoryToFilter : "All",
    stateToFilter : "All",
    textToFind: "",
    moviesLoaded: false,
    editMode: false,
    error : undefined
}