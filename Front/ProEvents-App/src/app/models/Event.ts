import { Batch } from "./Batch";
import { LecturerEvent } from "./LecturerEvent";
import { SocialNetwork } from "./SocialNetwork";

export interface Event {
  id: number;
  local: string;
  eventDate?: Date;
  theme: string;
  qtyPeople: number;
  imageURL: string;
  phone: string;
  email: string;
  batchs: Batch[];
  socialNetworks: SocialNetwork[];
  lecturerEvents: LecturerEvent[];


}
