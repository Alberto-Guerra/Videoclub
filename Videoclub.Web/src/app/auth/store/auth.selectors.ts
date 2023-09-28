import { createFeatureSelector, createSelector } from "@ngrx/store";
import { AuthState } from "./auth.state";

const selectAuthState = createFeatureSelector<AuthState>('auth');


export const isAuthenticated = createSelector(
    selectAuthState,
    (state: AuthState) => state.isAuthenticated
);

export const username = createSelector(
    selectAuthState,
    (state: AuthState) => state.username
);

export const userId = createSelector(
    selectAuthState,
    (state: AuthState) => state.userId
);






    
