import { createAction, props } from "@ngrx/store";
import { Movie, RentHistory } from "../movie.model";

export const LoadAllMovies = createAction('[Movies] Load All Movies');
export const LoadAllMoviesSuccess = createAction('[Movies] All Movies Loaded', props<{movies : Movie[]}> ());
export const LoadAllMoviesFail = createAction('[Movies] All Movies Load Failed', props<{error : string}> ());


export const LoadCategoriesSuccess = createAction('[Movies] Categories Loaded', props<{categories : string[]}> ());
export const LoadCategoriesFail = createAction('[Movies] Categories Load Failed', props<{error : string}> ());

export const FilterMovies = createAction('[Movies] Filter Movies',);

export const UpdateTextToFind = createAction('[Movies] Update Text To Find', props<{textToFind : string}> ());
export const UpdateCategoryToFilter = createAction('[Movies] Update Category To Filter', props<{categoryToFilter : string}> ());
export const UpdateStateToFilter = createAction('[Movies] Update State To Filter', props<{stateToFilter : string}> ());

export const SetSelectedMovie = createAction('[Movies] Set Selected Movie', props<{selectedMovie : Movie}> ());

export const logState = createAction('[Movies] Log State');
export const toggleEditMode = createAction('[Movies] Toggle Edit Mode');

export const RentMovie = createAction('[Movies] Rent Movie', props<{history : RentHistory}> ());
export const RentMovieSuccess = createAction('[Movies] Rent Movie Success', props<{movies : Movie[]}> ());
export const RentMovieFail = createAction('[Movies] Rent Movie Fail', props<{error : string}> ());

export const ReturnMovie = createAction('[Movies] Return Movie', props<{movie_id : number}> ());
export const ReturnMovieSuccess = createAction('[Movies] Return Movie Success', props<{movies : Movie[]}> ());
export const ReturnMovieFail = createAction('[Movies] Return Movie Fail', props<{error : string}> ());

export const UpdateMovie = createAction('[Movies] Update Movie', props<{movie : Movie}> ());
export const UpdateMovieSuccess = createAction('[Movies] Update Movie Success', props<{movies : Movie[]}> ());
export const UpdateMovieFail = createAction('[Movies] Update Movie Fail', props<{error : string}> ());

export const CreateMovie = createAction('[Movies] Create Movie', props<{movie : Movie}> ());
export const CreateMovieSuccess = createAction('[Movies] Create Movie Success', props<{movies : Movie[]}> ());
export const CreateMovieFail = createAction('[Movies] Create Movie Fail', props<{error : string}> ());

export const DeleteMovie = createAction('[Movies] Delete Movie', props<{movie_id : number}> ());
export const DeleteMovieSuccess = createAction('[Movies] Delete Movie Success', props<{movies : Movie[]}> ());
export const DeleteMovieFail = createAction('[Movies] Delete Movie Fail', props<{error : string}> ());
