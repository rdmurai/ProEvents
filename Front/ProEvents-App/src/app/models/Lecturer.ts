import { Event } from "./Event";
import { SocialNetwork } from "./SocialNetwork";

export interface Lecturer {
  id:number;
  name:string;
  resume:string;
  iamgeUrl:string;
  phone:string;
  email:string;
  socialNetworks:SocialNetwork[];
  lecturerEvents:Event[];


}
