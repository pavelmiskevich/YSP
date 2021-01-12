import { Component } from '@angular/core';

import { Summary } from "./model/summary.model";

@Component({
  selector: 'app-root',  
  // templateUrl: './app.component.html',
  
  template: "<router-outlet></router-outlet>"

  //template: '<button (click)="add()">Add</button><app-todos [todos]="todos"></app-todos>'
  //template: "<dynamic-item></dynamic-item>"
  // template: "<new-query></new-query>"

  //template: "<app-category></app-category>"
  //template: "<jqxTree #myTree [width]="300" [source]="records"></jqxTree>"
  //template: "<router-outlet></router-outlet>"
  ,styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'YSPWebAngular10';

  constructor(private summary: Summary) { }

  todos = [{ title: 'One' }, { title: 'Two' }];

  add() {
    this.todos = [...this.todos, { title: 'Three' }];
  }

} 