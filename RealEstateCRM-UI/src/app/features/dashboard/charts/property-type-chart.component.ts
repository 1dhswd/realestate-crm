import { Component, OnInit } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { DashboardService } from '../../../core/services/dashboard.service';

@Component({
  selector: 'app-property-type-chart',
  standalone: true,
  imports: [BaseChartDirective],
  template: `
    <canvas
      baseChart
      [type]="'bar'"
      [data]="chartData">
    </canvas>
  `
})
export class PropertyTypeChartComponent implements OnInit {
  chartData: ChartConfiguration<'bar'>['data'] = {
    labels: [],
    datasets: [
      { data: [], label: 'Emlak Sayısı' }
    ]
  };

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.dashboardService.getPropertyTypeChart().subscribe({
      next: (data) => {
        this.chartData.labels = data.map(x => x.type);
        this.chartData.datasets[0].data = data.map(x => x.count);
      },
      error: (err) => console.error('Chart error:', err)
    });
  }
}
