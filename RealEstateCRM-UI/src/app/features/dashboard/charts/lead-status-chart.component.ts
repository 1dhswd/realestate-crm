import { Component, OnInit } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { DashboardService } from '../../../core/services/dashboard.service';

@Component({
  selector: 'app-lead-status-chart',
  standalone: true,
  imports: [BaseChartDirective],
  template: `
    <canvas
      baseChart
      [type]="'pie'"
      [data]="pieChartData">
    </canvas>
  `
})
export class LeadStatusChartComponent implements OnInit {
  pieChartData: ChartConfiguration<'pie'>['data'] = {
    labels: [],
    datasets: [{ data: [] }]
  };

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.dashboardService.getLeadStatusChart().subscribe({
      next: (data) => {
        this.pieChartData.labels = data.map(x => x.status);
        this.pieChartData.datasets[0].data = data.map(x => x.count);
      },
      error: (err) => console.error('Chart error:', err)
    });
  }
}
