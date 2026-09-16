import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService, Ticket } from '../services/ticket.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-dashboard.html'
})
export class EmployeeDashboardComponent implements OnInit {
  tickets: Ticket[] = [];
  title = '';
  description = '';
  priority = 'Medium';

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

  createTicket(): void {
    if (!this.title) return;

    this.ticketService.createTicket({
      title: this.title,
      description: this.description,
      priority: this.priority
    }).subscribe(() => {
      this.title = '';
      this.description = '';
      this.priority = 'Medium';
      this.loadTickets();
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}