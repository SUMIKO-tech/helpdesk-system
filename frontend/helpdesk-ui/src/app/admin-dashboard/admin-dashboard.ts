import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService, Ticket } from '../services/ticket.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.html'
})
export class AdminDashboardComponent implements OnInit {
  tickets: Ticket[] = [];
  statuses = ['Open', 'InProgress', 'Resolved', 'Closed'];
  priorities = ['Low', 'Medium', 'High'];

  constructor(
    private ticketService: TicketService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.ticketService.getTickets().subscribe(data => this.tickets = data);
  }

  onStatusChange(ticket: Ticket, newStatus: string): void {
    this.ticketService.updateStatus(ticket.ticketId, newStatus).subscribe(() => this.loadTickets());
  }

  onPriorityChange(ticket: Ticket, newPriority: string): void {
    this.ticketService.updatePriority(ticket.ticketId, newPriority).subscribe(() => this.loadTickets());
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}