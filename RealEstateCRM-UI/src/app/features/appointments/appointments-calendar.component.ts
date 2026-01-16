import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FullCalendarModule } from '@fullcalendar/angular';

import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';

import { CalendarOptions } from '@fullcalendar/core';

@Component({
  selector: 'app-appointments-calendar',
  standalone: true,
  imports: [
    CommonModule,
    FullCalendarModule 
  ],
  templateUrl: './appointments-calendar.component.html',
  styleUrls: ['./appointments-calendar.component.scss']
})
export class AppointmentsCalendarComponent {

  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    editable: true,
    selectable: true,
    events: []
  };

}
