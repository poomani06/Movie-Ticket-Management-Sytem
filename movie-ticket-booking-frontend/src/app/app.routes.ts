import { Routes } from '@angular/router';

import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Movies } from './pages/movies/movies';
import { Shows } from './pages/shows/shows';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: 'register',
    component: Register
  },
  {
    path: 'movies',
    component: Movies
  },
  {
    path: 'shows/:movieId',
    component: Shows
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];