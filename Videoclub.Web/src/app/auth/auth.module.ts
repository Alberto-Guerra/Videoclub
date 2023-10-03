import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthBarComponent } from './auth-bar/auth-bar.component';
import { AuthLoginComponent } from './auth-login/auth-login.component';
import { AuthRegisterComponent } from './auth-register/auth-register.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { authReducers } from './store/auth.reducers';
import { EffectsModule } from '@ngrx/effects';
import { AuthEffects } from './store/auth.effects';
import { moviesReducers } from '../movies/store/movies.reducers';
import { RouterModule } from '@angular/router';
import { AuthRoutingModule } from './auth-routing.module';

@NgModule({
  declarations: [AuthBarComponent, AuthLoginComponent, AuthRegisterComponent],
  imports: [
    CommonModule,
    MatSlideToggleModule,
    AuthRoutingModule,
    FormsModule,
    StoreModule.forFeature('auth', authReducers),
    StoreModule.forFeature('movies', moviesReducers),
    EffectsModule.forFeature([AuthEffects]),
    RouterModule,
  ],
  exports: [AuthBarComponent],
})
export class AuthModule {}
