import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Show } from '../../models/show.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShowService {

  private apiUrl = `${environment.apiUrl}/shows`;

  constructor(private http: HttpClient) {}

  getShowsByMovieId(movieId: number): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/movie/${movieId}`);
  }
}