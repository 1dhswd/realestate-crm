import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lead, CreateLeadRequest, UpdateLeadRequest } from '../models/lead.model';

@Injectable({
  providedIn: 'root'
})
export class LeadService {
  private apiUrl = 'https://localhost:44348/api/Leads';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Lead[]> {
    return this.http.get<Lead[]>(this.apiUrl);
  }

  getById(id: number): Observable<Lead> {
    return this.http.get<Lead>(`${this.apiUrl}/${id}`);
  }

  create(lead: CreateLeadRequest): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.apiUrl, lead);
  }

  update(id: number, lead: UpdateLeadRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, lead);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
