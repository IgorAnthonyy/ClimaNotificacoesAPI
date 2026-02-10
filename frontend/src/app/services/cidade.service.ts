import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cidade, CidadeRequest } from '../models/cidade.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CidadeService {
  private apiUrl = `${environment.apiUrl}/Cidade`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Cidade[]> {
    return this.http.get<Cidade[]>(this.apiUrl);
  }

  getById(id: number): Observable<Cidade> {
    return this.http.get<Cidade>(`${this.apiUrl}/${id}`);
  }

  create(cidade: CidadeRequest): Observable<Cidade> {
    return this.http.post<Cidade>(this.apiUrl, cidade);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getPrevisoes(cidadeId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${cidadeId}/previsoes`);
  }
}
