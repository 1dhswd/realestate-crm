import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ToastrService } from 'ngx-toastr';
import { CustomerService } from '../../../core/services/customer.service';
import { Customer } from '../../../core/models/customer.model';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatTooltipModule
  ],
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerListComponent implements OnInit {
  customers: Customer[] = [];
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phoneNumber', 'status', 'actions'];
  loading = false;

  constructor(
    private customerService: CustomerService,
    private router: Router,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loading = true;
    this.customerService.getAll().subscribe({
      next: (data) => {
        this.customers = data;
        this.loading = false;
      },
      error: (error) => {
        this.toastr.error('Failed to load customers');
        this.loading = false;
      }
    });
  }

  createCustomer(): void {
    this.router.navigate(['/customers/create']);
  }

  editCustomer(id: number): void {
    this.router.navigate(['/customers/edit', id]);
  }

  deleteCustomer(id: number): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.delete(id).subscribe({
        next: () => {
          this.toastr.success('Customer deleted successfully');
          this.loadCustomers();
        },
        error: (error) => {
          this.toastr.error('Failed to delete customer');
        }
      });
    }
  }
}
