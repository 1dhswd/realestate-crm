import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PropertyImage, PropertyImageUploadResponse } from '../models/property-image.model';

@Injectable({
  providedIn: 'root'
})
export class PropertyImageService {
  private apiUrl = 'https://localhost:44348/api/properties';

  constructor(private http: HttpClient) { }

  getPropertyImages(propertyId: number): Observable<PropertyImage[]> {
    return this.http.get<PropertyImage[]>(`${this.apiUrl}/${propertyId}/images`);
  }

  uploadImages(propertyId: number, files: File[]): Observable<PropertyImageUploadResponse> {
    const formData = new FormData();
    files.forEach(file => {
      formData.append('files', file, file.name);
    });
    return this.http.post<PropertyImageUploadResponse>(`${this.apiUrl}/${propertyId}/images`, formData);
  }

  setPrimaryImage(propertyId: number, imageId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${propertyId}/images/${imageId}/set-primary`, {});
  }

  reorderImages(propertyId: number, imageIds: number[]): Observable<any> {
    return this.http.put(`${this.apiUrl}/${propertyId}/images/reorder`, imageIds);
  }

  deleteImage(propertyId: number, imageId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${propertyId}/images/${imageId}`);
  }

  getImageUrl(imageUrl: string): string {
    if (!imageUrl) return '';
    if (imageUrl.startsWith('http')) return imageUrl;
    return `https://localhost:44348${imageUrl}`;
  }
}
