import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DoCheck, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html'
  ,changeDetection: ChangeDetectionStrategy.OnPush
  //,styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit, DoCheck, OnChanges {
  @Input() categories;

  constructor(private cdr: ChangeDetectorRef) { 
      //this.cdr.detach();
  }  

  ngOnInit(): void {
      //this.cdr.detectChanges();
  }

  ngOnChanges(changes: SimpleChanges): void {
      //console.log('OnChanges CategoryListComponent');      
  }

  ngDoCheck(): void {
      //console.log('DoCheck CategoryListComponent');
      this.cdr.detectChanges();
      // this.cdr.reattach();
      //   setTimeout(() => {
      //       this.cdr.detach();
      //   })
  }

}
