import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';
import { LoginRequest, RegisterRequest, AuthResponse } from '../models/auth.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44348/api/Auth';
  private currentUserSubject: BehaviorSubject<User | null>;
  public currentUser: Observable<User | null>;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    const token = localStorage.getItem('token');
    this.currentUserSubject = new BehaviorSubject<User | null>(
      token ? this.getUserFromToken(token) : null
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials)
      .pipe(
        map(response => {
          localStorage.setItem('token', response.token);
          const user = this.convertToUser(response);
          this.currentUserSubject.next(user);
          return response;
        })
      );
  }

  register(data: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, data)
      .pipe(
        map(response => {
          localStorage.setItem('token', response.token);
          const user = this.convertToUser(response);
          this.currentUserSubject.next(user);
          return response;
        })
      );
  }

  private convertToUser(response: AuthResponse): User {
    const [firstName, ...lastNameParts] = response.fullName.split(' ');
    return {
      id: response.userId,
      username: '',
      email: response.email,
      firstName: firstName || '',
      lastName: lastNameParts.join(' ') || '',
      role: response.roles[0] || 'User'
    };
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token && !this.isTokenExpired(token);
  }

  private isTokenExpired(token: string): boolean {
    try {
      const decoded: any = jwtDecode(token);
      return decoded.exp < Date.now() / 1000;
    } catch {
      return true;
    }
  }

  private getUserFromToken(token: string): User | null {
    try {
      const decoded: any = jwtDecode(token);
      return {
        id: decoded.sub,
        username: decoded.username,
        email: decoded.email,
        firstName: decoded.firstName,
        lastName: decoded.lastName,
        role: decoded.role
      };
    } catch {
      return null;
    }
  }
}
