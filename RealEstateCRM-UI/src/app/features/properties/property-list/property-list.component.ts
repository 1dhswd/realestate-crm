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
import { PropertyService } from '../../../core/services/property.service';
import { Property } from '../../../core/models/property.model';
import { PropertyFilter } from '../../../core/models/property-filter.model';
import { PropertyFilterComponent } from '../property-filter/property-filter.component';

@Component({
  selector: 'app-property-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatTooltipModule,
    PropertyFilterComponent
  ],
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.scss']
})
export class PropertyListComponent implements OnInit {
  properties: Property[] = [];
  filteredProperties: Property[] = [];
  displayedColumns: string[] = ['title', 'city', 'price', 'area', 'type', 'status', 'actions'];
  loading = false;
  currentFilter: PropertyFilter = {};

  constructor(
    private propertyService: PropertyService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    this.loading = true;
    this.propertyService.getAll().subscribe({
      next: (data) => {
        this.properties = data;
        this.filteredProperties = data;
        this.loading = false;
      },
      error: (error) => {
        this.toastr.error('Failed to load properties');
        this.loading = false;
      }
    });
  }

  onFilterChange(filter: PropertyFilter): void {
    this.currentFilter = filter;
    this.applyFilters();
  }

  applyFilters(): void {
    let filtered = [...this.properties];

    // Search term
    if (this.currentFilter.searchTerm) {
      const term = this.currentFilter.searchTerm.toLowerCase();
      filtered = filtered.filter(p =>
        p.title.toLowerCase().includes(term) ||
        (p.description && p.description.toLowerCase().includes(term))
      );
    }

    // Price range
    if (this.currentFilter.minPrice !== null && this.currentFilter.minPrice !== undefined) {
      filtered = filtered.filter(p => p.price >= this.currentFilter.minPrice!);
    }
    if (this.currentFilter.maxPrice !== null && this.currentFilter.maxPrice !== undefined) {
      filtered = filtered.filter(p => p.price <= this.currentFilter.maxPrice!);
    }

    // Area range
    if (this.currentFilter.minArea !== null && this.currentFilter.minArea !== undefined) {
      filtered = filtered.filter(p => p.area >= this.currentFilter.minArea!);
    }
    if (this.currentFilter.maxArea !== null && this.currentFilter.maxArea !== undefined) {
      filtered = filtered.filter(p => p.area <= this.currentFilter.maxArea!);
    }

    // City
    if (this.currentFilter.city) {
      filtered = filtered.filter(p => p.city === this.currentFilter.city);
    }

    // Type IDs
    if (this.currentFilter.typeIds && this.currentFilter.typeIds.length > 0) {
      filtered = filtered.filter(p => this.currentFilter.typeIds!.includes(p.typeId!));
    }

    // Category IDs
    if (this.currentFilter.categoryIds && this.currentFilter.categoryIds.length > 0) {
      filtered = filtered.filter(p => this.currentFilter.categoryIds!.includes(p.categoryId!));
    }

    // Room count
    if (this.currentFilter.roomCount !== null && this.currentFilter.roomCount !== undefined) {
      filtered = filtered.filter(p => p.roomCount === this.currentFilter.roomCount);
    }

    // Bathroom count
    if (this.currentFilter.bathroomCount !== null && this.currentFilter.bathroomCount !== undefined) {
      filtered = filtered.filter(p => p.bathroomCount === this.currentFilter.bathroomCount);
    }

    // Active status
    if (this.currentFilter.isActive !== null && this.currentFilter.isActive !== undefined) {
      filtered = filtered.filter(p => p.isActive === this.currentFilter.isActive);
    }

    this.filteredProperties = filtered;
  }

  createProperty(): void {
    this.router.navigate(['/properties/create']);
  }

  editProperty(id: number): void {
    this.router.navigate(['/properties/edit', id]);
  }

  deleteProperty(id: number): void {
    if (confirm('Are you sure you want to delete this property?')) {
      this.propertyService.delete(id).subscribe({
        next: () => {
          this.toastr.success('Property deleted successfully');
          this.loadProperties();
        },
        error: (error) => {
          this.toastr.error('Failed to delete property');
        }
      });
    }
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('tr-TR', {
      style: 'currency',
      currency: 'TRY',
      minimumFractionDigits: 0
    }).format(price);
  }
}
