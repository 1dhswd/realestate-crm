import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './core/services/auth.service';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { FullCalendarModule } from '@fullcalendar/angular';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent, SidebarComponent, FullCalendarModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'RealEstateCRM';

  constructor(
    public authService: AuthService,
    private router: Router
  ) { }

  get isAuthPage(): boolean {
    return this.router.url === '/login' || this.router.url === '/register';
  }
}
