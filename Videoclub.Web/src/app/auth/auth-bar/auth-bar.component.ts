import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as AuthSelectors from '../../auth/store/auth.selectors';
import * as AuthActions from '../../auth/store/auth.actions';
import * as MoviesSelectors from '../../movies/store/movies.selectors';
import * as MoviesActions from '../../movies/store/movies.actions';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-bar',
  templateUrl: './auth-bar.component.html',
  styleUrls: ['./auth-bar.component.css'],
})
export class AuthBarComponent {
  editMode: boolean = false;
  isAuthenticated: boolean = false;
  username?: string = 'Test User';
  userId?: string = '1';

  constructor(private store: Store<AppState>, private router: Router) {
    //subscribe to the store to get the username and the user id
    store.select(AuthSelectors.username).subscribe((username) => {
      this.username = username;
    });
    store.select(AuthSelectors.userId).subscribe((userId) => {
      this.userId = userId;
    });
  }

  ngOnInit(): void {
    //subscribe to the store to get the edit mode and the authentication status
    this.store
      .select(AuthSelectors.isAuthenticated)
      .subscribe((isAuthenticated) => {
        this.isAuthenticated = isAuthenticated;
      });

    this.store.select(MoviesSelectors.editMode).subscribe((editMode) => {
      this.editMode = editMode;
    });
  }

  //dispatch the action to toggle the edit mode
  toggleEditMode() {
    this.store.dispatch(MoviesActions.toggleEditMode());
  }

  //dispatch the action to logout the user then sets the edit mode to false and navigate to the movies page
  logout() {
    this.store.dispatch(AuthActions.logout());
    if (this.editMode) {
      //in case we are in edit mode when doing logout, we need to toggle it off
      this.store.dispatch(MoviesActions.toggleEditMode());
    }
    this.router.navigate(['/movies']);
  }
}
