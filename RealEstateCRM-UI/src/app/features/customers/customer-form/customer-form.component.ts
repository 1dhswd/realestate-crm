import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { ToastrService } from 'ngx-toastr';
import { CustomerService } from '../../../core/services/customer.service';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.scss']
})
export class CustomerFormComponent implements OnInit {
  customerForm!: FormGroup;
  loading = false;
  isEditMode = false;
  customerId?: number;

  statuses = [
    { id: 1, name: 'Active' },
    { id: 2, name: 'Inactive' },
    { id: 3, name: 'Potential' }
  ];

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.initForm();

    this.customerId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.customerId) {
      this.isEditMode = true;
      this.loadCustomer(this.customerId);
    }
  }

  initForm(): void {
    this.customerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: [''],
      address: [''],
      statusId: [1, Validators.required],
      assignedAgentId: [null]
    });
  }

  loadCustomer(id: number): void {
    this.loading = true;
    this.customerService.getById(id).subscribe({
      next: (customer) => {
        this.customerForm.patchValue(customer);
        this.loading = false;
      },
      error: (error) => {
        this.toastr.error('Failed to load customer');
        this.loading = false;
        this.router.navigate(['/customers']);
      }
    });
  }

  onSubmit(): void {
    if (this.customerForm.invalid) {
      return;
    }

    this.loading = true;
    const formData = this.customerForm.value;

    if (this.isEditMode && this.customerId) {
      this.customerService.update(this.customerId, { ...formData, id: this.customerId }).subscribe({
        next: () => {
          this.toastr.success('Customer updated successfully');
          this.router.navigate(['/customers']);
        },
        error: (error) => {
          this.toastr.error('Failed to update customer');
          this.loading = false;
        }
      });
    } else {
      this.customerService.create(formData).subscribe({
        next: () => {
          this.toastr.success('Customer created successfully');
          this.router.navigate(['/customers']);
        },
        error: (error) => {
          this.toastr.error('Failed to create customer');
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/customers']);
  }
}
