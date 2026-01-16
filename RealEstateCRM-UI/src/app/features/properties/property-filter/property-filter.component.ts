import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatSliderModule } from '@angular/material/slider';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { PropertyFilter } from '../../../core/models/property-filter.model';

@Component({
  selector: 'app-property-filter',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatSliderModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './property-filter.component.html',
  styleUrls: ['./property-filter.component.scss']
})
export class PropertyFilterComponent implements OnInit {
  @Output() filterChange = new EventEmitter<PropertyFilter>();

  filterForm!: FormGroup;
  isFilterOpen = false;

  cities = ['Istanbul', 'Ankara', 'Izmir', 'Antalya', 'Bursa'];

  propertyTypes = [
    { id: 1, name: 'Apartment' },
    { id: 2, name: 'Villa' },
    { id: 3, name: 'Office' },
    { id: 4, name: 'Land' }
  ];

  propertyCategories = [
    { id: 1, name: 'For Sale' },
    { id: 2, name: 'For Rent' }
  ];

  roomOptions = [1, 2, 3, 4, 5, 6];
  bathroomOptions = [1, 2, 3, 4, 5];

  priceRange = { min: 0, max: 10000000 };
  areaRange = { min: 0, max: 1000 };

  activeFiltersCount = 0;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
    this.setupFormListeners();
  }

  initForm(): void {
    this.filterForm = this.fb.group({
      searchTerm: [''],
      minPrice: [null],
      maxPrice: [null],
      minArea: [null],
      maxArea: [null],
      city: [''],
      typeIds: [[]],
      categoryIds: [[]],
      roomCount: [null],
      bathroomCount: [null],
      isActive: [null]
    });
  }

  setupFormListeners(): void {
    this.filterForm.valueChanges.subscribe(() => {
      this.calculateActiveFilters();
    });
  }

  calculateActiveFilters(): void {
    const values = this.filterForm.value;
    let count = 0;

    if (values.searchTerm) count++;
    if (values.minPrice !== null && values.minPrice !== undefined) count++;
    if (values.maxPrice !== null && values.maxPrice !== undefined) count++;
    if (values.minArea !== null && values.minArea !== undefined) count++;
    if (values.maxArea !== null && values.maxArea !== undefined) count++;
    if (values.city) count++;
    if (values.typeIds && values.typeIds.length > 0) count++;
    if (values.categoryIds && values.categoryIds.length > 0) count++;
    if (values.roomCount !== null && values.roomCount !== undefined) count++;
    if (values.bathroomCount !== null && values.bathroomCount !== undefined) count++;
    if (values.isActive !== null && values.isActive !== undefined) count++;

    this.activeFiltersCount = count;
  }

  onTypeChange(typeId: number, checked: boolean): void {
    const typeIds = this.filterForm.get('typeIds')?.value || [];
    if (checked) {
      typeIds.push(typeId);
    } else {
      const index = typeIds.indexOf(typeId);
      if (index > -1) {
        typeIds.splice(index, 1);
      }
    }
    this.filterForm.patchValue({ typeIds });
  }

  onCategoryChange(categoryId: number, checked: boolean): void {
    const categoryIds = this.filterForm.get('categoryIds')?.value || [];
    if (checked) {
      categoryIds.push(categoryId);
    } else {
      const index = categoryIds.indexOf(categoryId);
      if (index > -1) {
        categoryIds.splice(index, 1);
      }
    }
    this.filterForm.patchValue({ categoryIds });
  }

  isTypeSelected(typeId: number): boolean {
    const typeIds = this.filterForm.get('typeIds')?.value || [];
    return typeIds.includes(typeId);
  }

  isCategorySelected(categoryId: number): boolean {
    const categoryIds = this.filterForm.get('categoryIds')?.value || [];
    return categoryIds.includes(categoryId);
  }

  applyFilters(): void {
    const filters: PropertyFilter = {
      ...this.filterForm.value
    };
    this.filterChange.emit(filters);
  }

  clearFilters(): void {
    this.filterForm.reset({
      searchTerm: '',
      minPrice: null,
      maxPrice: null,
      minArea: null,
      maxArea: null,
      city: '',
      typeIds: [],
      categoryIds: [],
      roomCount: null,
      bathroomCount: null,
      isActive: null
    });
    this.applyFilters();
  }

  formatPrice(value: number): string {
    if (value >= 1000000) {
      return `${(value / 1000000).toFixed(1)}M`;
    } else if (value >= 1000) {
      return `${(value / 1000).toFixed(0)}K`;
    }
    return value.toString();
  }
}
