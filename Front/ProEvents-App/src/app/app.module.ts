import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EventDetailsComponent } from './components/events/event-details/event-details.component';
import { EventListComponent } from './components/events/event-list/event-list.component';
import { EventsComponent } from './components/events/events.component';
import { LecturesComponent } from './components/lectures/lectures.component';
import { LoginComponent } from './components/user/login/login.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { UserComponent } from './components/user/user.component';
import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';
import { BatchService } from './services/batch.service';
import { EventService } from './services/event.service';
import { NavComponent } from './shared/nav/nav.component';
import { TitleComponent } from './shared/title/title.component';

@NgModule({
  declarations: [
    AppComponent,
    EventsComponent,
    LecturesComponent,
    NavComponent,
    ProfileComponent,
    DashboardComponent,
    ContactsComponent,
    TitleComponent,
    DateTimeFormatPipe,
    EventDetailsComponent,
    EventListComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    FormsModule,
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    NgxSpinnerModule,
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),


  ],
  providers: [EventService, BatchService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
