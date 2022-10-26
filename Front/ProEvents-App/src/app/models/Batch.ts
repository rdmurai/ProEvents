export interface Batch {
  id:number;
  name:string;
  price:number;
  dateStart?:Date;
  dateEnd?:Date;
  quantity:number;

  eventId:number;
  event:Event;
}
