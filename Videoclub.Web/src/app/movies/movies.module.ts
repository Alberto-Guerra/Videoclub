import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AllMoviesComponent } from './all-movies/all-movies.component';
import { DetailedMovieComponent } from './detailed-movie/detailed-movie.component';
import {HttpClientModule} from '@angular/common/http';
import { MoviesRoutingModule } from './movies-routing.module';
import {MatCardModule} from '@angular/material/card';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatIconModule} from '@angular/material/icon';
import {FormsModule} from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { moviesReducers } from './store/movies.reducers';
import { MoviesEffects } from './store/movies.effects';
import { FormMovieComponent } from './form-movie/form-movie.component';
import { EditMovieComponent } from './edit-movie/edit-movie.component';
import { AddMovieComponent } from './add-movie/add-movie.component';





@NgModule({
  declarations: [
    AllMoviesComponent,
    DetailedMovieComponent,
    FormMovieComponent,
    EditMovieComponent,
    AddMovieComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MoviesRoutingModule,
    MatCardModule,
    MatSidenavModule,
    MatIconModule,
    FormsModule,
    StoreModule.forFeature('movies', moviesReducers),
    EffectsModule.forFeature([MoviesEffects]),
  ]
})
export class MoviesModule { }
