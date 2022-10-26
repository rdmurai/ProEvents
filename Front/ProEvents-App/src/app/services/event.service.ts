import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Event } from "../models/Event";

@Injectable()
export class EventService {

baseURL = 'https://localhost:5001/api/events';

constructor(private http: HttpClient) {}

public getEvent():Observable<Event[]>{
  return this.http.get<Event[]>(this.baseURL).pipe(take(1));
}

public getElementsByTheme(theme:string):Observable<Event[]>{
  return this.http.get<Event[]>(`${this.baseURL}/theme/${theme}`).pipe(take(1));
}


public getEventById(id:number):Observable<Event>{
  return this.http.get<Event>(`${this.baseURL}/${id}`).pipe(take(1));
}

public post(event:Event):Observable<Event>{
  return this.http.post<Event>(this.baseURL, event).pipe(take(1));
}

public put(event:Event):Observable<Event>{
  return this.http.put<Event>(`${this.baseURL}/${event.id}`, event).pipe(take(1));
}
public delete(id:number):Observable<boolean>{
  return this.http.delete<boolean>(`${this.baseURL}/${id}`).pipe(take(1));
}
}
