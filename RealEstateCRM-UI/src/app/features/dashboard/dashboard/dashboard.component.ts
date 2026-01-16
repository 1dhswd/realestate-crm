import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { DashboardService, DashboardStats } from '../../../core/services/dashboard.service';
import { AppointmentMonthlyChartComponent } from '../charts/appointment-monthly-chart.component';
import { LeadStatusChartComponent } from '../charts/lead-status-chart.component';
import { PropertyTypeChartComponent } from '../charts/property-type-chart.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    AppointmentMonthlyChartComponent,
    LeadStatusChartComponent,
    PropertyTypeChartComponent
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  stats: DashboardStats = {
    totalProperties: 0,
    totalCustomers: 0,
    totalLeads: 0,
    totalAppointments: 0,
    activeProperties: 0,
    pendingLeads: 0
  };

  loading = true;

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.dashboardService.getMyStats().subscribe({
      next: (data: DashboardStats) => {
        this.stats = data;
        this.loading = false;
      },
      error: (error: any) => {
        console.error('Failed to load dashboard stats:', error);
        this.loading = false;
      }
    });
  }
}
