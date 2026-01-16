import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ToastrService } from 'ngx-toastr';
import { LeadService } from '../../../core/services/lead.service';
import { CustomerService } from '../../../core/services/customer.service';
import { PropertyService } from '../../../core/services/property.service';

@Component({
  selector: 'app-lead-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './lead-form.component.html',
  styleUrls: ['./lead-form.component.scss']
})
export class LeadFormComponent implements OnInit {
  leadForm!: FormGroup;
  loading = false;
  isEditMode = false;
  leadId?: number;

  customers: any[] = [];
  properties: any[] = [];
  statuses = [
    { id: 1, name: 'New Lead' },
    { id: 2, name: 'Contacted' },
    { id: 3, name: 'Qualified' },
    { id: 4, name: 'Proposal Sent' },
    { id: 5, name: 'Won' },
    { id: 6, name: 'Lost' }
  ];

  constructor(
    private fb: FormBuilder,
    private leadService: LeadService,
    private customerService: CustomerService,
    private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.loadCustomers();
    this.loadProperties();

    this.leadId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.leadId) {
      this.isEditMode = true;
      this.loadLead(this.leadId);
    }
  }

  initForm(): void {
    this.leadForm = this.fb.group({
      customerId: [null, Validators.required],
      propertyId: [null],
      statusId: [1, Validators.required],
      budget: [null],
      notes: [''],
      nextFollowUpDate: [null]
    });
  }

  loadCustomers(): void {
    this.customerService.getAll().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (error) => {
        console.error('Failed to load customers:', error);
      }
    });
  }

  loadProperties(): void {
    this.propertyService.getAll().subscribe({
      next: (data) => {
        this.properties = data;
      },
      error: (error) => {
        console.error('Failed to load properties:', error);
      }
    });
  }

  loadLead(id: number): void {
    this.loading = true;
    this.leadService.getById(id).subscribe({
      next: (lead) => {
        this.leadForm.patchValue({
          customerId: lead.customerId,
          propertyId: lead.propertyId,
          statusId: lead.statusId,
          budget: lead.budget,
          notes: lead.notes,
          nextFollowUpDate: lead.nextFollowUpDate
        });
        this.loading = false;
      },
      error: (error) => {
        this.toastr.error('Failed to load lead');
        this.loading = false;
        this.router.navigate(['/leads']);
      }
    });
  }

  onSubmit(): void {
    if (this.leadForm.invalid) {
      this.toastr.error('Please fill all required fields');
      return;
    }

    this.loading = true;
    const formData = this.leadForm.value;

    if (this.isEditMode && this.leadId) {
      this.leadService.update(this.leadId, { ...formData, id: this.leadId }).subscribe({
        next: () => {
          this.toastr.success('Lead updated successfully');
          this.router.navigate(['/leads']);
        },
        error: (error) => {
          console.error('Update error:', error);
          this.toastr.error(error?.error?.message || 'Failed to update lead');
          this.loading = false;
        }
      });
    } else {
      this.leadService.create(formData).subscribe({
        next: () => {
          this.toastr.success('Lead created successfully');
          this.router.navigate(['/leads']);
        },
        error: (error) => {
          console.error('Create error:', error);
          this.toastr.error(error?.error?.message || 'Failed to create lead');
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/leads']);
  }
}
