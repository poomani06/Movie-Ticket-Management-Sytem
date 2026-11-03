import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Seat } from '../../models/seat.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SeatService {

  private apiUrl = `${environment.apiUrl}/seats`;

  constructor(private http: HttpClient) {}

  getSeatsByScreenId(screenId: number): Observable<Seat[]> {
    return this.http.get<Seat[]>(`${this.apiUrl}/screen/${screenId}`);
  }
}