import * as AuthActions from './auth.actions';

import { createReducer, on, Action } from '@ngrx/store';
import { initialState } from './auth.state';

export const authReducers = createReducer(
    initialState,

    on(AuthActions.loginSuccess, (state, { username, userId }) => {
        return {
            ...state,
            isAuthenticated: true,
            username: username,
            userId: userId,
            error: null
        };
    }),

    on(AuthActions.loginFail, (state, {error} ) => {
        return {
            ...state,
            isAuthenticated: false,
            username: undefined,
            userId: undefined,
            error: error
        };
    }),


    on(AuthActions.registerSuccess, (state, { username }) => {
        alert(username + " is successfully registered!!");
        return {
            ...state,
            error: null
        };
    }),

    on(AuthActions.registerFail, (state, {error} ) => {
        return {
            ...state,
            isAuthenticated: false,
            username: undefined,
            userId: undefined,
            error: error
        };
    }),

    on(AuthActions.logout, (state) => {
        return {
            ...state,
            isAuthenticated: false,
            username: undefined,
            userId: undefined,
            error: undefined
        };
    }),

    on(AuthActions.autoLoginSuccess, (state, { username, userId }) => {
        return {
            ...state,
            isAuthenticated: true,
            username: username,
            userId: userId,
            error: null
        };
    }),

    on(AuthActions.autoLoginFail, (state, {error} ) => {
        
        return {
            ...state,
            isAuthenticated: false,
            username: undefined,
            userId: undefined,
            error: error
        };
    }),

    on(AuthActions.clearError, (state) => {
        return {
            ...state,
            error: undefined
        };
    }),

    




  

    
    
    
    );