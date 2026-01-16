import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule, MatListModule, MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  menuItems = [
    { path: '/dashboard', label: 'Dashboard', icon: 'dashboard' },
    { path: '/customers', label: 'Customers', icon: 'people' },
    { path: '/leads', label: 'Leads', icon: 'assignment' },
    { path: '/properties', label: 'Properties', icon: 'home' }
  ];

  constructor(private router: Router) { }

  isActive(path: string): boolean {
    return this.router.url.startsWith(path);
  }
}
