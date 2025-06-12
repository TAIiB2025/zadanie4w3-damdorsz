import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Film } from '../models/film';
import { Observable } from 'rxjs';
import { FilmBody } from '../models/film-body';

@Injectable({
  providedIn: 'root'
})
export class ListaService {
  private readonly apiUrl = 'http://localhost:5006/api/Film';

  constructor(private readonly http: HttpClient) {}

  get(fraza?: string): Observable<Film[]> {
    let params = new HttpParams();
    if (fraza && fraza.trim()) {
      params = params.set('fraza', fraza.trim());
    }
    return this.http.get<Film[]>(this.apiUrl, { params });
  }

  getByID(id: number): Observable<Film> {
    return this.http.get<Film>(`${this.apiUrl}/${id}`);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  put(id: number, body: FilmBody): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, body);
  }

  post(body: FilmBody): Observable<void> {
    return this.http.post<void>(this.apiUrl, body);
  }
}