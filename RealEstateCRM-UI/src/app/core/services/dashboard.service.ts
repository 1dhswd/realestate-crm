import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DashboardStats {
  totalProperties: number;
  totalCustomers: number;
  totalLeads: number;
  totalAppointments: number;
  activeProperties: number;
  pendingLeads: number;
}

export interface ChartData {
  labels: string[];
  data: number[];
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = 'https://localhost:44348/api/Dashboard'; 

  constructor(private http: HttpClient) { }

  getMyStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.apiUrl}/my-stats`);
  }

  getAppointmentMonthlyChart(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/charts/appointments-monthly`);
  }

  getLeadStatusChart(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/charts/lead-status`);
  }

  getPropertyTypeChart(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/charts/property-type`);
  }
}
