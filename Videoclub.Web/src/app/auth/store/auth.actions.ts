import { createAction, props } from '@ngrx/store';
import { UserDto } from '../auth.model';

export const loginStart = createAction( '[Auth] Login Start', props<{ user: string; password: string }>());
export const loginSuccess = createAction( '[Auth] Login Success', props<{ username: string; userId: string }>());
export const loginFail = createAction( '[Auth] Login Fail', props<{ error: string }>());

export const logout = createAction( '[Auth] Logout' );
export const logoutSuccess = createAction( '[Auth] Logout Success' );
export const logoutFail = createAction( '[Auth] Logout Fail', props<{ error: string }>());

export const registerStart = createAction( '[Auth] Register Start', props<{ user : UserDto }>());
export const registerSuccess = createAction( '[Auth] Register Success', props<{ username: string; }>());
export const registerFail = createAction( '[Auth] Register Fail', props<{ error: string }>());

export const autoLogin = createAction( '[Auth] Set Token', props<{ token: string }>());
export const autoLoginSuccess = createAction( '[Auth] Auto Login Success', props<{ username: string; userId: string }>());
export const autoLoginFail = createAction( '[Auth] Auto Login Fail', props<{ error: string }>());

export const clearError = createAction( '[Auth] Clear Error' );


