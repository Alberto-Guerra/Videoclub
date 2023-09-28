import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DetailedMovieComponent } from "./detailed-movie/detailed-movie.component";
import { AllMoviesComponent } from "./all-movies/all-movies.component";
import { FormMovieComponent } from "./form-movie/form-movie.component";
import { EditMovieComponent } from "./edit-movie/edit-movie.component";
import { AddMovieComponent } from "./add-movie/add-movie.component";

const routes : Routes = [
    
    {path: 'edit/:id', component : EditMovieComponent},
    {path: 'add', component : AddMovieComponent},
    {path: ':id', component : DetailedMovieComponent},
    {path: '', pathMatch : "full", component : AllMoviesComponent},
    {path: '**', redirectTo : ''}
]
  
@NgModule({
declarations: [],
exports: [RouterModule],
imports: [
    CommonModule,
    RouterModule.forChild(routes)
]
})
export class MoviesRoutingModule { }