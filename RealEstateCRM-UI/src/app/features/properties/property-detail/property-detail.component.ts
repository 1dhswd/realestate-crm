import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PropertyService } from '../../../core/services/property.service';
import { Property } from '../../../core/models/property.model';
import { PropertyImageUploadComponent } from '../property-image-upload/property-image-upload.component';

@Component({
  selector: 'app-property-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatTabsModule,
    MatProgressSpinnerModule,
    PropertyImageUploadComponent
  ],
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.scss']
})
export class PropertyDetailComponent implements OnInit {
  property?: Property;
  loading = false;
  propertyId!: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private propertyService: PropertyService
  ) { }

  ngOnInit(): void {
    this.propertyId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.propertyId) {
      this.loadProperty();
    }
  }

  loadProperty(): void {
    this.loading = true;
    this.propertyService.getById(this.propertyId).subscribe({
      next: (property) => {
        this.property = property;
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load property:', error);
        this.loading = false;
        this.router.navigate(['/properties']);
      }
    });
  }

  editProperty(): void {
    this.router.navigate(['/properties/edit', this.propertyId]);
  }

  goBack(): void {
    this.router.navigate(['/properties']);
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('tr-TR', {
      style: 'currency',
      currency: 'TRY'
    }).format(price);
  }
}
