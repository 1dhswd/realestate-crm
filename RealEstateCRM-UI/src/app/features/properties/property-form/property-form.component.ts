import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ToastrService } from 'ngx-toastr';
import { PropertyService } from '../../../core/services/property.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-property-form',
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
    MatCheckboxModule
  ],
  templateUrl: './property-form.component.html',
  styleUrls: ['./property-form.component.scss']
})
export class PropertyFormComponent implements OnInit {
  propertyForm!: FormGroup;
  loading = false;
  isEditMode = false;
  propertyId?: number;
  currentUserId?: number;

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

  cities = ['Istanbul', 'Ankara', 'Izmir', 'Antalya', 'Bursa'];

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    const currentUser = this.authService.currentUserValue;
    if (!currentUser?.id) {
      this.toastr.error('User not authenticated');
      this.router.navigate(['/login']);
      return;
    }
    this.currentUserId = currentUser.id;

    this.initForm();

    this.propertyId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.propertyId) {
      this.isEditMode = true;
      this.loadProperty(this.propertyId);
    }
  }

  initForm(): void {
    this.propertyForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      price: [0, [Validators.required, Validators.min(0)]],
      address: ['', Validators.required],
      city: ['', Validators.required],
      district: [''],
      typeId: [1, Validators.required],
      categoryId: [1, Validators.required],
      area: [0, [Validators.required, Validators.min(0)]],
      roomCount: [null],
      bathroomCount: [null],
      floorNumber: [null],
      buildingAge: [null],
      isActive: [true]
    });
  }

  loadProperty(id: number): void {
    this.loading = true;
    this.propertyService.getById(id).subscribe({
      next: (property) => {
        this.propertyForm.patchValue({
          title: property.title,
          description: property.description,
          price: property.price,
          address: property.address,
          city: property.city,
          district: property.district,
          typeId: property.typeId,
          categoryId: property.categoryId,
          area: property.area,
          roomCount: property.roomCount,
          bathroomCount: property.bathroomCount,
          floorNumber: property.floorNumber,
          buildingAge: property.buildingAge,
          isActive: property.isActive
        });
        this.loading = false;
      },
      error: (error) => {
        console.error('Load error:', error);
        this.toastr.error('Failed to load property');
        this.loading = false;
        this.router.navigate(['/properties']);
      }
    });
  }

  onSubmit(): void {
    if (this.propertyForm.invalid) {
      this.toastr.error('Please fill all required fields');
      return;
    }

    if (!this.currentUserId) {
      this.toastr.error('User not authenticated');
      return;
    }

    this.loading = true;

    if (this.isEditMode && this.propertyId) {
      const updateData = {
        id: this.propertyId,
        title: this.propertyForm.value.title,
        description: this.propertyForm.value.description || '',
        price: this.propertyForm.value.price,
        area: this.propertyForm.value.area,
        roomCount: this.propertyForm.value.roomCount,
        bathroomCount: this.propertyForm.value.bathroomCount,
        floorNumber: this.propertyForm.value.floorNumber,
        buildingAge: this.propertyForm.value.buildingAge,
        address: this.propertyForm.value.address,
        city: this.propertyForm.value.city,
        district: this.propertyForm.value.district || '',
        latitude: undefined,
        longitude: undefined,
        isActive: this.propertyForm.value.isActive,
        isFeatured: false,
        categoryId: this.propertyForm.value.categoryId,
        typeId: this.propertyForm.value.typeId,
        featureIds: []
      };

      console.log('Updating property:', updateData);

      this.propertyService.update(this.propertyId, updateData).subscribe({
        next: () => {
          this.toastr.success('Property updated successfully');
          this.router.navigate(['/properties']);
        },
        error: (error) => {
          console.error('Update error:', error);
          this.toastr.error(error?.error?.message || 'Failed to update property');
          this.loading = false;
        }
      });
    } else {
      const createData = {
        title: this.propertyForm.value.title,
        description: this.propertyForm.value.description || '',
        price: this.propertyForm.value.price,
        area: this.propertyForm.value.area,
        roomCount: this.propertyForm.value.roomCount,
        bathroomCount: this.propertyForm.value.bathroomCount,
        floorNumber: this.propertyForm.value.floorNumber,
        buildingAge: this.propertyForm.value.buildingAge,
        address: this.propertyForm.value.address,
        city: this.propertyForm.value.city,
        district: this.propertyForm.value.district || '',
        latitude: undefined,
        longitude: undefined,
        categoryId: this.propertyForm.value.categoryId,
        typeId: this.propertyForm.value.typeId,
        ownerId: this.currentUserId,
        featureIds: [],
        imageUrls: []
      };

      console.log('Creating property:', createData);
      console.log('Current User ID:', this.currentUserId);

      this.propertyService.create(createData).subscribe({
        next: () => {
          this.toastr.success('Property created successfully');
          this.router.navigate(['/properties']);
        },
        error: (error) => {
          console.error('Create error:', error);
          this.toastr.error(error?.error?.message || 'Failed to create property');
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/properties']);
  }
}
