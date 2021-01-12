import { AfterViewInit, Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[duplicateContent]'
  })
  export class DuplicateContentDirective implements AfterViewInit{
    @Input() duplicateContent: boolean = false;
  
    private _contentWasDuplicated: boolean = false;
  
    constructor(private tpl: TemplateRef<any>, private vc: ViewContainerRef){
      this.vc.createEmbeddedView(this.tpl);
    }
  
    ngAfterViewInit(){
      if(this.duplicateContent && !this._contentWasDuplicated) {
        this.vc.insert(this.vc.createEmbeddedView(this.tpl));
        this._contentWasDuplicated = true;
      }
    }
  }