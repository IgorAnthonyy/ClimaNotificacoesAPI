import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PrevisaoTempo } from '../models/previsao.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PrevisaoService {
  private apiUrl = `${environment.apiUrl}/PrevisaoTempo`;

  constructor(private http: HttpClient) { }

  getPrevisao(cidadeId: number): Observable<PrevisaoTempo> {
    return this.http.post<PrevisaoTempo>(`${this.apiUrl}/${cidadeId}`, {});
  }
}
