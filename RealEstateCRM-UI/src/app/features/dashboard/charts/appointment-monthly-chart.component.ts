import { Component, OnInit } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { DashboardService } from '../../../core/services/dashboard.service';

@Component({
  selector: 'app-appointment-monthly-chart',
  standalone: true,
  imports: [BaseChartDirective],
  template: `
    <canvas
      baseChart
      [type]="'bar'"
      [data]="barChartData">
    </canvas>
  `
})
export class AppointmentMonthlyChartComponent implements OnInit {
  barChartData: ChartConfiguration<'bar'>['data'] = {
    labels: [],
    datasets: [
      { data: [], label: 'Randevu Sayısı' }
    ]
  };

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.dashboardService.getAppointmentMonthlyChart().subscribe({
      next: (data) => {
        this.barChartData.labels = data.map(x => x.month);
        this.barChartData.datasets[0].data = data.map(x => x.count);
      },
      error: (err) => console.error('Chart error:', err)
    });
  }
}
