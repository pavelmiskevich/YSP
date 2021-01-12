import { Component, ComponentFactoryResolver, ViewChild, ViewContainerRef } from '@angular/core';
import { DynamicItemComponent } from './dynamic-item.component';

@Component({
    selector: 'dynamic-list',
    templateUrl: './dynamic-list.component.html'
  })
  export class DynamicListComponent {
    //@ViewChild('sample') sample: any;
    @ViewChild('sample', { read: ViewContainerRef }) myRef
  
    constructor(private componentFactoryResolver: ComponentFactoryResolver){}
  
    addEntity(){
        //this.sample.viewContainerRef.clear();
      this.myRef.clear();
  
      let dynamicItemComponent = this.componentFactoryResolver.resolveComponentFactory(DynamicItemComponent);
      //let dynamicItemComponentRef = this.sample.viewContainerRef.createComponent(dynamicItemComponent);
      let dynamicItemComponentRef = this.myRef.createComponent(dynamicItemComponent);
  
      //(<DynamicItemComponent>dynamicItemComponentRef.dynamicItemComponentRef).value = {
        (<DynamicItemComponent>dynamicItemComponentRef.instance).value = {
        title: 'Title',
        text: 'Text'
      };
    }
  }