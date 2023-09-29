import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as AuthActions from '../store/auth.actions';
import * as AuthSelectors from '../store/auth.selectors';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';

@Component({
  selector: 'app-auth-login',
  templateUrl: './auth-login.component.html',
  styleUrls: ['./auth-login.component.css'],
})
export class AuthLoginComponent {
  username: string = '';
  password: string = '';

  errorString: string = '';

  constructor(
    private store: Store<AppState>,
    private router: Router,
    private actions$: Actions
  ) {
    this.store.dispatch(AuthActions.clearError());
  }

  //on submit, dispatch the action to login the user
  onSubmit() {
    this.store.dispatch(
      AuthActions.loginStart({ user: this.username, password: this.password })
    );

    //In case of error, display the error message
    this.actions$.pipe(ofType(AuthActions.loginFail)).subscribe((action) => {
      this.errorString = action.error;
    });

    //In case of success, navigate to the movies page
    this.actions$.pipe(ofType(AuthActions.loginSuccess)).subscribe(() => {
      this.router.navigate(['/movies']);
    });
  }
}
