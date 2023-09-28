import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { catchError, map, mergeMap } from 'rxjs/operators';
import {merge, of} from 'rxjs';
import { AuthService } from '../auth.service';
import * as AuthActions from './auth.actions';
import { UserDto } from '../auth.model';

@Injectable()
export class AuthEffects {
    constructor(private actions$: Actions,private service: AuthService) {}   
    

    loginStart$ = createEffect(() =>
    this.actions$.pipe(
        ofType(AuthActions.loginStart),
        mergeMap((action) => {
            return this.service.login(action.user, action.password).pipe(
                map((user) =>
                    AuthActions.loginSuccess({username: user.username, userId: user.userId})
                ),
                catchError((error) => of(AuthActions.loginFail({ error: error.error }))))
            })));

    registerStart$ = createEffect(() =>
    this.actions$.pipe(
        ofType(AuthActions.registerStart),
        mergeMap((action) => {
            return this.service.register(action.user).pipe(
                map((username) =>
                    AuthActions.registerSuccess({username: username})
                ),
                catchError((error) => of(AuthActions.registerFail({ error: error.error }))))
            })));

    autoLogin$ = createEffect(() =>
    this.actions$.pipe(
        ofType(AuthActions.autoLogin),
        mergeMap((action) => {
            return this.service.autoLogin(action.token).pipe(
                map((user) =>
                    AuthActions.autoLoginSuccess({username: user.username, userId: user.userId})
                ),
                catchError((error) => of(AuthActions.autoLoginFail({ error: error.error }))))
            })));

    logout$ = createEffect(() =>
    this.actions$.pipe(
        ofType(AuthActions.logout),
        mergeMap(() => {
            return this.service.logout().pipe(
                map(() =>
                    AuthActions.logoutSuccess()
                ),
                catchError((error) => of(AuthActions.logoutFail({ error: error.error }))))
            })));



    
    
}
