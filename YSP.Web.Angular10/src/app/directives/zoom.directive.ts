import {Directive, ElementRef, HostListener, Input} from '@angular/core';
    
@Directive({
    selector: '[zoom]'
})
export class ZoomDirective{
    @Input('zoom') size;
    private _defaultSize: number;
  
    constructor(private el: ElementRef){
      this._defaultSize = this.el.nativeElement.style.fontSize.replace('px', '');
    }
  
    @HostListener('mouseover') onMouseEnter(){
      this.setFontSize(this.size);
    }
  
    @HostListener('mouseout') onMouseLeave(){
      this.setFontSize(this._defaultSize);
    }
  
    setFontSize(size: number | string): void{
      this.el.nativeElement.style.fontSize = `${size}px`;
    }
  }