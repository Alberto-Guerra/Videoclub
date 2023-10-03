import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, throwError } from 'rxjs';
import { environment } from 'src/enviroments/enviroment';
import { UserDto } from './auth.model';
import {
  HttpClient,
  HttpHeaders,
} from '@angular/common/http';
import jwt_decode from 'jwt-decode';
import { Store } from '@ngrx/store';
import { AppState } from '../store/app.state';

import * as AuthActions from './store/auth.actions';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authString = 'auth';
  private token?: string;

  constructor(private http: HttpClient, private store: Store<AppState>) {}

  //call the API to login with the username and password passed, treating the token in the response
  login(
    username: string,
    password: string
  ): Observable<{ username: string; userId: string }> {
    const loginData = { username, password };

    return this.http
      .post(environment.apiUrl + this.authString + '/login', loginData, {
        responseType: 'text',
      })
      .pipe(
        map((response) => {
          //decode the token to get the username and the userId
          let decodedToken: any = jwt_decode(response);
          let usernameFromToken = decodedToken.name;
          let userId = decodedToken.id;

          this.setToken(response); //then set the token in the local storage and in the service
          return { username: usernameFromToken, userId };
        })
      );
  }

  //login directly with the token passed
  autoLogin(token: string): Observable<{ username: string; userId: string }> {
    //decode the token to get the username and the userId
    let decodedToken: any = jwt_decode(token);
    let username = decodedToken.name;
    let userId = decodedToken.id;

    this.token = token; //then set the token in the service
    return of({ username, userId });
  }

  //call the API to register the user passed, adapting the date to the format needed
  register(user: UserDto): Observable<string> {
    let date: string = new Date(user.birthdate).toISOString(); //need to convert the date to a string in the format yyyy-MM-dd

    let userToPass = {
      username: user.username,
      password: user.password,
      name: user.name,
      lastname: user.lastname,
      birthday: date,
    };

    return this.http.post(
      environment.apiUrl + this.authString + '/register',
      userToPass,
      { responseType: 'text' }
    );
  }

  //removes the token from the local storage and from the service to logout
  logout(): Observable<void> {
    this.removeToken();
    return of();
  }

  //set the token in the local storage and in the service
  setToken(token: string) {
    localStorage.setItem('token', token);

    this.token = token;
  }
  //remove the token from the local storage and from the service
  removeToken() {
    localStorage.removeItem('token');
    this.token = undefined;
  }

  //check if there is a token stored and then if its not expired to login with it
  checkStoredToken() {
    let tokenFromStorage = localStorage.getItem('token');

    if (tokenFromStorage) {
      if (!this.isTokenExpired(tokenFromStorage)) {
        this.store.dispatch(AuthActions.autoLogin({ token: tokenFromStorage })); //If it is not expired, dispatch the autoLogin action
        this.token = tokenFromStorage;
      }
    }
  }

  //check if the token is expired
  private isTokenExpired(token: string): boolean {
    const decodedToken: any = jwt_decode(token);
    if (decodedToken && decodedToken.exp) {
      //check if the token is valid and has an expiration time
      const expirationTime = decodedToken.exp * 1000; //convert to milliseconds
      const currentTime = Date.now(); //get the current time in milliseconds
      return expirationTime < currentTime; //if the expiration time is less than the current time, the token is expired
    }
    return true;
  }

  //if the service has a token, return the headers with the token
  getHeaders() {
    if (!this.token) {
      return new HttpHeaders();
    }
    return new HttpHeaders({ Authorization: `Bearer ${this.token}` });
  }
}
