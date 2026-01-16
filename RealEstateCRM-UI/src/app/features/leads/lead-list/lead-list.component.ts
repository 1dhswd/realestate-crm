import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ToastrService } from 'ngx-toastr';
import { LeadService } from '../../../core/services/lead.service';
import { Lead } from '../../../core/models/lead.model';

@Component({
  selector: 'app-lead-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTooltipModule,
    MatChipsModule
  ],
  templateUrl: './lead-list.component.html',
  styleUrls: ['./lead-list.component.scss']
})
export class LeadListComponent implements OnInit {
  leads: Lead[] = [];
  displayedColumns: string[] = ['customerName', 'propertyTitle', 'statusName', 'budget', 'nextFollowUpDate', 'actions'];


  loading = false;

  constructor(
    private leadService: LeadService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadLeads();
  }

  loadLeads(): void {
    this.loading = true;
    this.leadService.getAll().subscribe({
      next: (data) => {
        this.leads = data;
        this.loading = false;
      },
      error: (error) => {
        this.toastr.error('Failed to load leads');
        this.loading = false;
      }
    });
  }

  createLead(): void {
    this.router.navigate(['/leads/create']);
  }

  editLead(id: number): void {
    this.router.navigate(['/leads/edit', id]);
  }

  deleteLead(id: number): void {
    if (confirm('Are you sure you want to delete this lead?')) {
      this.leadService.delete(id).subscribe({
        next: () => {
          this.toastr.success('Lead deleted successfully');
          this.loadLeads();
        },
        error: (error) => {
          this.toastr.error('Failed to delete lead');
        }
      });
    }
  }

  getStatusColor(status: string): string {
    const colors: { [key: string]: string } = {
      'New': 'primary',
      'Contacted': 'accent',
      'Qualified': 'warn',
      'Converted': 'primary',
      'Lost': ''
    };
    return colors[status] || '';
  }
}
