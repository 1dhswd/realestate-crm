import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer, CreateCustomerRequest, UpdateCustomerRequest } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = 'https://localhost:44348/api/Customers';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  }

  getById(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/${id}`);
  }

  create(customer: CreateCustomerRequest): Observable<Customer> {
    return this.http.post<Customer>(this.apiUrl, customer);
  }

  update(id: number, customer: UpdateCustomerRequest): Observable<Customer> {
    return this.http.put<Customer>(`${this.apiUrl}/${id}`, customer);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
