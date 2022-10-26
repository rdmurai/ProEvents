import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.css']
})
export class TitleComponent implements OnInit {
  @Input() title: string = '';
  @Input() subtitle : string = 'Since 2022';
  @Input() iconClass : string = 'fa fa-user';
  @Input() listButton: boolean = false

  constructor(private router:Router) { }

  ngOnInit():void {
  }

  list(){
    this.router.navigate([`/${this.title.toLowerCase()}/list`]);

  }

}
