import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Ticket {
  ticketId: number;
  title: string;
  description?: string;
  priority: string;
  status: string;
  createdByName: string;
  createdDate: string;
}

export interface CreateTicketRequest {
  title: string;
  description?: string;
  priority: string;
}

@Injectable({ providedIn: 'root' })
export class TicketService {
  private baseUrl = 'https://localhost:7164/api/tickets';

  constructor(private http: HttpClient) {}

  getTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl);
  }

  createTicket(request: CreateTicketRequest): Observable<any> {
    return this.http.post(this.baseUrl, request);
  }

  updateStatus(id: number, status: string): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}/status`, { status });
  }

  updatePriority(id: number, priority: string): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}/priority`, { priority });
  }
}