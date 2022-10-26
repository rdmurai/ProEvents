import { Lecturer } from "./Lecturer";

export interface LecturerEvent {
  lecturerId:number;
  lecturer:Lecturer;
  eventId:number;
  event:Event;
}
