import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property, CreatePropertyRequest, UpdatePropertyRequest } from '../models/property.model';
import { PropertyFilter } from '../models/property-filter.model';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private apiUrl = 'https://localhost:44348/api/Properties';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Property[]> {
    return this.http.get<Property[]>(this.apiUrl);
  }

  getFiltered(filter: PropertyFilter): Observable<Property[]> {
    let params = new HttpParams();

    if (filter.searchTerm) {
      params = params.set('searchTerm', filter.searchTerm);
    }
    if (filter.minPrice !== undefined && filter.minPrice !== null) {
      params = params.set('minPrice', filter.minPrice.toString());
    }
    if (filter.maxPrice !== undefined && filter.maxPrice !== null) {
      params = params.set('maxPrice', filter.maxPrice.toString());
    }
    if (filter.minArea !== undefined && filter.minArea !== null) {
      params = params.set('minArea', filter.minArea.toString());
    }
    if (filter.maxArea !== undefined && filter.maxArea !== null) {
      params = params.set('maxArea', filter.maxArea.toString());
    }
    if (filter.city) {
      params = params.set('city', filter.city);
    }
    if (filter.typeIds && filter.typeIds.length > 0) {
      filter.typeIds.forEach(id => {
        params = params.append('typeIds', id.toString());
      });
    }
    if (filter.categoryIds && filter.categoryIds.length > 0) {
      filter.categoryIds.forEach(id => {
        params = params.append('categoryIds', id.toString());
      });
    }
    if (filter.roomCount !== undefined && filter.roomCount !== null) {
      params = params.set('roomCount', filter.roomCount.toString());
    }
    if (filter.bathroomCount !== undefined && filter.bathroomCount !== null) {
      params = params.set('bathroomCount', filter.bathroomCount.toString());
    }
    if (filter.isActive !== undefined && filter.isActive !== null) {
      params = params.set('isActive', filter.isActive.toString());
    }
    if (filter.sortBy) {
      params = params.set('sortBy', filter.sortBy);
    }
    if (filter.sortDirection) {
      params = params.set('sortDirection', filter.sortDirection);
    }

    return this.http.get<Property[]>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Property> {
    return this.http.get<Property>(`${this.apiUrl}/${id}`);
  }

  create(property: CreatePropertyRequest): Observable<any> {
    return this.http.post(this.apiUrl, property);
  }

  update(id: number, property: UpdatePropertyRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, property);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
