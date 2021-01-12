import { Component } from '@angular/core';

@Component({
    selector: 'dynamic-item',
    templateUrl: './dynamic-item.component.html'
  })
  export class DynamicItemComponent {
    value: any = null;
  
    constructor(){}
  }