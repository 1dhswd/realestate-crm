import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ToastrService } from 'ngx-toastr';
import { PropertyImageService } from '../../../core/services/property-image.service';
import { PropertyImage } from '../../../core/models/property-image.model';

@Component({
  selector: 'app-property-image-upload',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './property-image-upload.component.html',
  styleUrls: ['./property-image-upload.component.scss']
})
export class PropertyImageUploadComponent implements OnInit {
  @Input() propertyId!: number;

  images: PropertyImage[] = [];
  selectedFiles: File[] = [];
  previewUrls: string[] = [];
  uploading = false;
  loading = false;
  dragOver = false;

  constructor(
    private propertyImageService: PropertyImageService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    if (this.propertyId) {
      this.loadImages();
    }
  }

  loadImages(): void {
    this.loading = true;
    this.propertyImageService.getPropertyImages(this.propertyId).subscribe({
      next: (images) => {
        this.images = images;
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load images:', error);
        this.loading = false;
      }
    });
  }

  onFileSelected(event: any): void {
    const files: FileList = event.target.files;
    this.handleFiles(files);
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.dragOver = true;
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.dragOver = false;
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.dragOver = false;

    const files = event.dataTransfer?.files;
    if (files) {
      this.handleFiles(files);
    }
  }

  handleFiles(files: FileList): void {
    const validFiles: File[] = [];
    const maxSize = 5 * 1024 * 1024;
    const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png', 'image/gif', 'image/webp'];

    for (let i = 0; i < files.length; i++) {
      const file = files[i];

      if (!allowedTypes.includes(file.type)) {
        this.toastr.error(`${file.name} is not a valid image type`);
        continue;
      }

      if (file.size > maxSize) {
        this.toastr.error(`${file.name} exceeds 5MB limit`);
        continue;
      }

      validFiles.push(file);

      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.previewUrls.push(e.target.result);
      };
      reader.readAsDataURL(file);
    }

    this.selectedFiles = [...this.selectedFiles, ...validFiles];
  }

  removeSelectedFile(index: number): void {
    this.selectedFiles.splice(index, 1);
    this.previewUrls.splice(index, 1);
  }

  uploadImages(): void {
    if (this.selectedFiles.length === 0) {
      this.toastr.warning('Please select images to upload');
      return;
    }

    this.uploading = true;
    this.propertyImageService.uploadImages(this.propertyId, this.selectedFiles).subscribe({
      next: (response) => {
        this.toastr.success(response.message);
        this.selectedFiles = [];
        this.previewUrls = [];
        this.loadImages();
        this.uploading = false;
      },
      error: (error) => {
        console.error('Upload error:', error);
        this.toastr.error(error?.error?.message || 'Failed to upload images');
        this.uploading = false;
      }
    });
  }

  setPrimaryImage(imageId: number): void {
    this.propertyImageService.setPrimaryImage(this.propertyId, imageId).subscribe({
      next: () => {
        this.toastr.success('Primary image updated');
        this.loadImages();
      },
      error: (error) => {
        this.toastr.error('Failed to set primary image');
      }
    });
  }

  deleteImage(imageId: number): void {
    if (!confirm('Are you sure you want to delete this image?')) {
      return;
    }

    this.propertyImageService.deleteImage(this.propertyId, imageId).subscribe({
      next: () => {
        this.toastr.success('Image deleted successfully');
        this.loadImages();
      },
      error: (error) => {
        this.toastr.error('Failed to delete image');
      }
    });
  }

  getImageUrl(imageUrl: string): string {
    return this.propertyImageService.getImageUrl(imageUrl);
  }
}
