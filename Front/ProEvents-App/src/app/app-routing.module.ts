import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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

const routes: Routes = [
  {path:'events',redirectTo:'events/list'},
  {path: 'events', component: EventsComponent,
  children: [
    {path: 'detail/:id', component: EventDetailsComponent},
    {path: 'detail', component: EventDetailsComponent},
    {path: 'list', component: EventListComponent}
  ]
},
{path: 'dashboard', component: DashboardComponent},
{path: 'contact', component: ContactsComponent},
{path: 'profile', component: ProfileComponent},
{path: 'lectures', component: LecturesComponent},
{path: '', redirectTo: 'dashboard', pathMatch:'full'},
{path:'user', component: UserComponent,
children: [
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent}
]
},
{path: '**', redirectTo: 'dashboard', pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
