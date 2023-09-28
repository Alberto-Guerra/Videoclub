import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as AuthActions from '../store/auth.actions';
import { UserDto } from '../auth.model';
import * as AuthSelectors from '../store/auth.selectors';
import { Observable, takeWhile } from 'rxjs';
import { Actions, ofType } from '@ngrx/effects';

@Component({
  selector: 'app-auth-register',
  templateUrl: './auth-register.component.html',
  styleUrls: ['./auth-register.component.css']
})
export class AuthRegisterComponent {

  //fields
  username : string = '';
  password : string = '';
  name : string = '';
  lastname : string = '';
  birthdate : Date = new Date();

  errorString : string = '';

  constructor(private store : Store<AppState>, private router : Router, private actions$ : Actions) {
    this.store.dispatch(AuthActions.clearError());
   }

  onSubmit() {
    let user : UserDto = {
      username : this.username,
      password : this.password,
      name : this.name,
      lastname : this.lastname,
      birthdate : this.birthdate
    }

    this.store.dispatch(AuthActions.registerStart( {user : user} ));
    
    // In case of error, display the error message
    this.actions$.pipe(ofType(AuthActions.registerFail)).subscribe((action) => {
      this.errorString = action.error;
    });

    // In case of success, navigate to the movies page
    this.actions$.pipe(ofType(AuthActions.registerSuccess)).subscribe((action) => {
      this.router.navigate(['/movies']);
    });

   

  }

}
