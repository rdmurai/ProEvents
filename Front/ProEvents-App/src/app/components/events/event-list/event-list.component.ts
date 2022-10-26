import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { EventService } from 'src/app/services/event.service';
import { Event } from "../../../models/Event";

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {

  modalRef?: BsModalRef;

  public events: Event[] = [];
  public eventsFiltered: Event[] = [];
  public marginImage = 2;
  public showImage = true;
  private _filter : string = "";
  public eventId = 0;


  public get filter():string {
    return this._filter;
  }

  public set filter(value:string){
    this._filter = value;
    this.eventsFiltered = this.filter ? this.filterEvents(this.filter) : this.events
  }

  public filterEvents(filterBy:string): Event[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      ev => ev.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
                  ev.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    )

  }

  constructor(
    private eventService: EventService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEvents();


  }

  public changeImageState():void{
    this.showImage = !this.showImage
  }

  public getEvents(): void{

    this.eventService.getEvent().subscribe( {
      next: (_event:Event[]) => {
        this.events = _event;
        this.eventsFiltered = this.events
      },
      error: (error:Error) => {
        this.toastr.error('Error while loading Events', 'Error!')}

    }).add(() => this.spinner.hide());

  }

  openModal(event:any, template: TemplateRef<any>, eventId: number):void {
    event.stopPropagation();
    this.eventId = eventId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventService.delete(this.eventId).subscribe({
      next: (res:boolean) => {
          this.toastr.success('Event successfully deleted!','Deleted!');
          this.getEvents();

      },
      error: (error:Error) => {
        this.toastr.error('Error while deleting Events', 'Error!')
      }
    }).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detailEvent(id:number):void {
    this.router.navigate([`events/detail/${id}`]);

  }


}
