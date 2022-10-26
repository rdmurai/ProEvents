import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Batch } from '../models/Batch';

@Injectable()
export class BatchService {

baseURL = 'https://localhost:5001/api/batchs';

head = new HttpHeaders({
  'Content-Type': 'application/json'
});


constructor(private http: HttpClient) {}

public getBatchByEventId(eventId:number):Observable<Batch[]>{
  return this.http.get<Batch[]>(`${this.baseURL}/${eventId}`).pipe(take(1));
}

public saveBatch(eventId:number, batchs:Batch[]):Observable<Batch[]>{
  return this.http.put<Batch[]>(`${this.baseURL}/${eventId}`, batchs, {headers: this.head}).pipe(take(1));
}
public deleteBatch(eventId: number, batchId:number):Observable<boolean>{
  return this.http.delete<boolean>(`${this.baseURL}/${eventId}/${batchId}`).pipe(take(1));
}
}
