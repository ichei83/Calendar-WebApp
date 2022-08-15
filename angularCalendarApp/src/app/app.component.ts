import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { CalendarOptions, DateSelectArg, EventClickArg, EventApi, EventAddArg, EventChangeArg, EventRemoveArg } from '@fullcalendar/angular';
import { HttpService } from 'src/app/services/http.service';
import { EventInput } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';  


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit {

  constructor(public httpService: HttpService) {
  }
  currentEvents: EventApi[] = [];
  initalEventsData: EventInput[] = [];
  calendarOptions: any;  

  options: any;  
  eventsModel: any[] = [];  
  calendarWeekends = true;  
  // calendarEvents: EventInput[] = [  
  //   { title: 'Event Now', start: new Date() }  
  // ];  

  async ngOnInit() {
    this.calendarOptions = {
      headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
      },
      plugins: [dayGridPlugin]  ,
      initialView: 'timeGridWeek',
      initialEvents: this.initalEventsData,
      weekends: true,
      editable: true,
      selectable: true,
      selectMirror: true,
      dayMaxEvents: true,
      disableDragging: true,
     // theme: 'bootstrap5',
      select: this.handleDateSelect.bind(this),
      eventClick: this.handleEventClick.bind(this),
      //eventsSet: this.handleEvents.bind(this),
      eventAdd: this.handleEventAdd.bind(this),
      eventChange: this.handleEventUpdate.bind(this),
      eventRemove: this.handleEventDelete.bind(this),
    };
    this.options = {  
      editable: false,  
      disableDragging: false,  
      selectable: true,  
      theme: 'standart',  
      header: {  
        right: 'prev,next, today',  
        left: '',  
      },  
      validRange: {  
        start: '2017-05-01',  
        end: '2019-06-01'  
      },  
      plugins: [dayGridPlugin]  
    }; 
    await this.initialEvents();

    this.calendarOptions.events = this.initalEventsData;
  }

  async ngAfterViewInit() {
    this.currentEvents = [];
    this.initalEventsData = [];
    let result3 = await <any>this.httpService.get('/Calendar/GetAllCalendarMongoAsync'); 
    result3.calendars.forEach(async (event: any) => {
      console.log(event);
      this.currentEvents.push(event);
    });

  }
  calendarVisible = true;


  handleCalendarToggle() {
    this.currentEvents = [];

    this.calendarVisible = !this.calendarVisible;
  }

  handleWeekendsToggle() {

    const { calendarOptions } = this;
    calendarOptions.weekends = !calendarOptions.weekends;
  }

  async handleDateSelect(selectInfo: DateSelectArg) {
    const title = prompt('Please enter event title');
    const calendarApi = selectInfo.view.calendar;

    calendarApi.unselect(); 
    let nextEventId = await <any>this.httpService.get('/Calendar/GetNextEventId'); 

    if (title) {
      let data = {
        eventId: nextEventId.eventID, //'', //createEventId(),
        title,
        start: selectInfo.startStr,
        end: selectInfo.endStr,
        allDay: selectInfo.allDay
      };
      calendarApi.addEvent(
        data
      );

    }
  }
  async refreshEvents() {
    this.currentEvents = [];
    let result3 = await <any>this.httpService.get('/Calendar/GetAllCalendarMongoAsync'); 
    result3.calendars.forEach(async (event: any) => {
      console.log(event);
      this.calendarOptions.events.push(event);
      this.currentEvents.push(event);
    });

  }

  async initialEvents() {
     let result3:any = await <any>this.httpService.get('/Calendar/GetAllCalendarMongoAsync'); 
     result3.calendars.forEach(async (event: any) => {
       console.log(event);
       this.initalEventsData.push(  {
        eventId: event.eventId,
        title: event.title,
        start: event.start,
        end: event.end
      })
     });

     return this.initalEventsData;
   }

  async handleEventClick(clickInfo: EventClickArg) {
    if (confirm(`Are you sure you want to delete the event '${clickInfo.event.title}'`)) {
      clickInfo.event.remove();
      console.log(clickInfo);
    }
  }

  async handleEvents(events: EventApi[]) {
    this.currentEvents = [];
    this.initalEventsData = [];
    let result3 = await <any>this.httpService.get('/Calendar/GetAllCalendarMongoAsync'); 
    result3.calendars.forEach(async (event: any) => {
      console.log(event);
      this.currentEvents.push(event);
    });
  }

  async handleEventAdd(arg: EventAddArg) {

    console.log('add: ' + arg.event);
    //let nextEventId = await <any>this.httpService.get('/Calendar/GetNextEventId'); 

    let data = {
      eventId: arg.event._def.extendedProps['eventId'], //nextEventId.eventID, //'',//arg.event._def.extendedProps['eventId'],
      title: arg.event.title,
      start: arg.event.startStr,
      end: arg.event.endStr,
      allDay: arg.event.allDay
    };
    
    await this.AddCalendar(data);
    await this.refreshEvents();
}

async handleEventUpdate(arg: EventChangeArg) {
     console.log('update: ' + arg.event);
     console.log('update: ' + arg.oldEvent._def.extendedProps['eventId']);
    // let nextEventId = await <any>this.httpService.get('/Calendar/GetNextEventId'); 

   //  arg.event._def.extendedProps['eventId'] = nextEventId.eventID; // await <any>this.httpService.get('/Calendar/GetNextEventId'); 

     let data = {
      eventId: arg.oldEvent._def.extendedProps['eventId'], //arg.event._def.extendedProps['eventId'],
      title: arg.event.title,
      start: arg.event.startStr,
      end: arg.event.endStr,
      allDay: arg.event.allDay
    };

    await this.UpdateCalendar(data);

  
 }

  // async handleEventAdd(arg: EventAddArg) {

  //     console.log('add: ' + arg.event);
  //     let data = {
  //       eventId: '',//arg.event._def.extendedProps['eventId'],
  //       title: arg.event.title,
  //       start: arg.event.startStr,
  //       end: arg.event.endStr,
  //       allDay: arg.event.allDay
  //     };
      
  //     await this.AddCalendar(data);
  //     await this.refreshEvents();
  // }

  // async handleEventUpdate(arg: EventChangeArg) {
  //      console.log('update: ' + arg.event);
  //      console.log('update: ' + arg.oldEvent._def.extendedProps['eventId']);
  //      let data = {
  //       eventId: arg.event._def.extendedProps['eventId'],
  //       title: arg.event.title,
  //       start: arg.event.startStr,
  //       end: arg.event.endStr,
  //       allDay: arg.event.allDay
  //     };

  //     await this.UpdateCalendar(data);

    
  //  }

   async handleEventDelete(arg: EventRemoveArg) {
       console.log('delete: ' + arg.event);
       await this.DeleteCalendar(arg.event._def.extendedProps['eventId']);
     
  }
   
  async AddCalendar(data: any){
    console.log(data);
    let result = await <any>this.httpService.post('/Calendar/AddCalendar', data); 
    console.log(result);
    await this.refreshEvents();

  }

  async UpdateCalendar(data: any){
    console.log(data);
    let result = await <any>this.httpService.post('/Calendar/UpdateCalendar', data); 
    console.log(result);
    await this.refreshEvents();

  }

  async DeleteCalendar(id: any){
    console.log(id);
    let result = await <any>this.httpService.delete('/Calendar/' + id); 
    console.log(result);
    await this.refreshEvents();

  }

  gotoPast() {  
      //let calendarApi = selectInfo.view.calendar;
//      this.calendarComponent.getApi();  
    //calendarApi.gotoDate('2000-01-01'); // call a method on the Calendar object  
  } 

  
}
