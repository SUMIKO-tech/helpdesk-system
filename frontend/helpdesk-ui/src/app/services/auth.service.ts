import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  name: string;
  role: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = 'https://localhost:7164/api/auth';

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, request).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('name', res.name);
        localStorage.setItem('role', res.role);
      })
    );
  }

  logout(): void {
    localStorage.clear();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}