import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BatchService } from 'src/app/services/batch.service';
import { EventService } from 'src/app/services/event.service';
import { Batch } from '../../../models/Batch';
import { Event } from '../../../models/Event';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.css']
})
export class EventDetailsComponent implements OnInit {
  form!: FormGroup;
  event = {} as Event;
  saveMode:boolean = true;
  eventId: number = 0;
  modalRef: BsModalRef;
  actualBatch = {id: 0, name:'', index:0};




  constructor(private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private eventService:EventService,
    private spinner: NgxSpinnerService,
    private toastr:ToastrService,
    private localeService:  BsLocaleService,
    private router: Router,
    private batchService: BatchService,
    private modalService: BsModalService
    ) {
      this.localeService.use('en');
    }

    ngOnInit() {
      this.loadEvent();
      this.validation();
    }

    get f(): any{
      return this.form.controls;
    }

    get editMode(): boolean{
      return this.saveMode == false;
    }

    get batchs(): FormArray{
      return this.form.get('batches') as FormArray;
    }

    get bsConfig(): any {
      return {isAnimated: true,
        adaptivePosition: true,
        dateInputFormat: 'DD/MM/YYYY hh:mm a',
        containerClass: 'theme-default',
        showWeekNumbers: false
      }

    }
    get bsConfiBatch(): any {
      return {isAnimated: true,
        adaptivePosition: true,
        dateInputFormat: 'DD/MM/YYYY',
        containerClass: 'theme-default',
        showWeekNumbers: false
      }

    }

    public validation(): void {
      this.form = this.fb.group(
        {

          theme:['', [Validators.required, Validators.minLength(4)]],
          local:['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
          eventDate:['', Validators.required],
          qtyPeople:['', [Validators.required, Validators.max(120000)]],
          imageURL:[''],
          phone:['', Validators.required],
          email:['', [Validators.required, Validators.email]],
          batches: this.fb.array([]),

        });
      }

      addBatch():void {
        this.batchs.push(
          this.createBatch({id: 0} as Batch)
          );
        }

        createBatch(batch: Batch):FormGroup{
          return this.fb.group({
            id:[batch.id],
            name:[batch.name, [Validators.required,  Validators.minLength(4)]],
            price:[batch.price, Validators.required],
            dateStart:[batch.dateStart],
            dateEnd:[batch.dateEnd],
            quantity:[batch.quantity, Validators.required]
          })
        }

        public resetForm():void{
          this.form.reset();

        }

        public cssValidator(fieldForm: FormControl | AbstractControl):any{
          return {'is-invalid': fieldForm.errors && fieldForm.touched};
        }

        public loadEvent():void{
          this.eventId = +this.activatedRouter.snapshot.paramMap.get('id');

          if (this.eventId != null && this.eventId != 0) {

            this.saveMode = true;
            this.eventService.getEventById(this.eventId).subscribe({
              next: (ev: Event) =>{
                this.event = {...ev};
                this.form.patchValue(this.event);
                this.loadBatch();
              },
              error: (error: any) => {
                this.spinner.hide();
                this.toastr.error('Error on loading Event', 'ERROR!');
                console.error(error);
              },
              complete: () => this.spinner.hide()
            });
          }

        }

        public loadBatch():void{
          this.batchService.getBatchByEventId(this.eventId).subscribe({
            next: (batchReturn: Batch[]) => {
              batchReturn.forEach(batch => {this.batchs.push(this.createBatch(batch))})
            },
            error:(error: any) => {
              this.toastr.error('Error while loading batchs', 'Error')
              console.log(error)
            },
            complete: () => {}

          }).add(() => this.spinner.hide())
        }

        public saveChanges():void {
          this.spinner.show();
          if (this.form.valid) {

            if (this.saveMode){

              this.event = {...this.form.value}
              this.eventService.post(this.event).subscribe({
                next:(eventReturn: Event) => {

                  this.toastr.success('Saved!', 'Success!');
                  this.router.navigate([`events/detail/${eventReturn.id}`]);
                },
                error:(error: Error) => {
                  this.spinner.hide();
                  this.toastr.error('Error while saving changes Event', 'ERROR!');
                  console.error(error);
                },
                complete:() => this.spinner.hide()
              })}

              else {

                this.event = {id: this.event.id, ...this.form.value}
                this.eventService.put(this.event).subscribe({
                  next:() => {this.toastr.success('Updated!', 'Success!')},
                  error:(error: Error) => {
                    this.spinner.hide();
                    this.toastr.error('Error while updating Event', 'ERROR!');
                    console.error(error);
                  },
                  complete:() => this.spinner.hide()
                })};


              }
            }

            public saveBatch():void{
              if (this.form.controls['batches'].valid) {
                this.spinner.show();
                this.batchService.saveBatch(this.eventId, this.form.value.batchs).subscribe({
                  next:() => {
                    this.toastr.success('Batchs Saved!', 'Success!')
                    this.batchs.reset()},
                    error:(error: any) => {
                      this.toastr.error('Error while saving batchs','Error!');
                      console.log(error) },
                      complete: () => {}
                    }).add(() => this.spinner.hide());
                  }
                }

                public deleteBatch(template:TemplateRef<any>, index:number ):void{
                  this.actualBatch.id = this.batchs.get(index + '.id').value;
                  this.actualBatch.name = this.batchs.get(index + '.name').value;
                  this.actualBatch.index = index;

                  this.modalRef = this.modalService.show(template, {class:'modal-sm'});


                }

                confirmDeleteBatch():void{
                  this.modalRef.hide();
                  this.spinner.show();
                  this.batchService.deleteBatch(this.eventId, this.actualBatch.id).subscribe({
                    next:() => {
                      this.toastr.success('Batch deleted!', 'Success!');
                      this.batchs.removeAt(this.actualBatch.index);
                    },
                    error:(error:any)=>{
                      this.toastr.error('Error when deleting Batch...', 'Error!');
                      console.error(error);
                    },
                    complete() {}
                  }).add(() => this.spinner.hide());
                }

                declineDeleteBatch():void{
                  this.modalRef.hide();
                }

                public changeDate(value:Date, index:number, field:string):void{
                  this.batchs.value[index][field] = value;
                }

                public returnTitleBatch(value: string):string{
                  return value === null || value === '' ? 'Batch Name' : value
                }


              }
