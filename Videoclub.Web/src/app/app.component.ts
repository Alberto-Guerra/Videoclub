import { Component } from '@angular/core';
import { AppState } from './store/app.state';
import { Store } from '@ngrx/store';
import jwt_decode from 'jwt-decode';

import * as AuthActions from './auth/store/auth.actions';
import { AuthService } from './auth/auth.service';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Videoclub.UI';
  
  editMode = false;

  constructor(authService : AuthService) {
    
   authService.checkStoredToken();
    
  }


}

