import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, throwError} from 'rxjs';
import { environment } from 'src/enviroments/enviroment';
import { UserDto } from './auth.model';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import jwt_decode from 'jwt-decode';
import { Store } from '@ngrx/store';
import { AppState } from '../store/app.state';

import * as AuthActions from './store/auth.actions';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authString = 'auth';
  private token? : string;

  constructor(private http : HttpClient, private store : Store<AppState>) {

    

   }

  login(username: string, password: string): Observable<{ username: string; userId: string }> {
    const loginData = { username, password };

    return this.http.post(environment.apiUrl + this.authString + '/login', loginData, { responseType: 'text' }).pipe(      
      map((response) => {
        //we decode the token to get the username and the userId
        let decodedToken : any = jwt_decode(response);
        let usernameFromToken = decodedToken.name;
        let userId = decodedToken.id;
        
        this.setToken(response); //then we set the token in the local storage
        return { username: usernameFromToken, userId };
      })
      
    );
  }

  autoLogin(token : string) : Observable<{ username: string; userId: string }> {
    //we decode the token to get the username and the userId
    let decodedToken : any = jwt_decode(token);
    let username = decodedToken.name;
    let userId = decodedToken.id;

    this.token = token; //then we set the token in the service
    return of({ username, userId });
  }

  register (user : UserDto) : Observable<string> {
    
    let date : string = new Date(user.birthdate).toISOString(); //we need to convert the date to a string in the format yyyy-MM-dd

    let userToPass = {username: user.username, password: user.password,  name: user.name, lastname: user.lastname, birthday: date}

    return this.http.post(environment.apiUrl + this.authString + '/register', userToPass, { responseType: 'text' })

  }

  
  logout () : Observable<void> {
    
    this.removeToken(); //we remove the token from the local storage and from the service
    return of();
  }

  //we set the token in the local storage and in the service
  setToken(token : string) {
    
    localStorage.setItem('token', token);
    
    this.token = token;

  }
  //we remove the token from the local storage and from the service
  removeToken() {
    
    localStorage.removeItem('token');
    this.token = undefined;
  }

  //we check if we have a token stored and if so we login with it
  checkStoredToken(){
    let tokenFromStorage = localStorage.getItem("token"); 
    
   
    if(tokenFromStorage) {
      if(!this.isTokenExpired(tokenFromStorage)) { //we check if it is expired.
        this.store.dispatch(AuthActions.autoLogin({ token: tokenFromStorage}));  //If it is not expired, we dispatch the autoLogin action
        this.token = tokenFromStorage;
      }
    }
  }

  //we check if the token is expired
  private isTokenExpired(token: string): boolean {
    const decodedToken: any = jwt_decode(token); 
    if (decodedToken && decodedToken.exp) {
      const expirationTime = decodedToken.exp * 1000; 
      const currentTime = Date.now();
      return expirationTime < currentTime;
    }
    return true; 
  }

  //if we have a token, we return the headers with the token
  getHeaders() {
    if(!this.token){
      return new HttpHeaders();
    }
    return new HttpHeaders({'Authorization': `Bearer ${this.token}`});

    
  }



}
