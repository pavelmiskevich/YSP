import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DoCheck, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Category } from '../model/category.model';
import { CategoryRepository } from '../model/category.repository';
import { ActivatedRoute, Router } from "@angular/router";
import { Summary } from '../model/summary.model';

@Component({
  selector: 'app-category-item'
  ,templateUrl: './category-item.component.html'
  ,changeDetection: ChangeDetectionStrategy.OnPush
  //,styleUrls: ['./category-item.component.css']
})
export class CategoryItemComponent implements OnInit, DoCheck, OnChanges {
  @Input() category;
  public isAuth = true;

  constructor(private activateRoute: ActivatedRoute, private cdr: ChangeDetectorRef, 
    private repository: CategoryRepository, private summary: Summary) { }

  ngOnChanges(changes: SimpleChanges): void {
    //console.log('OnChanges CategoryItemComponent');    
  }

  ngDoCheck(): void {
    //console.log('DoCheck CategoryItemComponent');
    this.cdr.detectChanges();
  }

  ngOnInit(): void {
      //this.cdr.detectChanges();
  }

  deactivate(entity: Category) {
    this.repository.deactivate(entity);
    this.cdr.detectChanges();
  }

  //delete(id: number) {
  delete(entity: Category) {
      this.repository.delete(entity.id);
      const index = this.repository.categories.indexOf(entity, 0);
      if (index > -1) {
        this.repository.categories.splice(index, 1);
      }
      this.summary.itemCount = this.repository.categories.length;
      this.cdr.detectChanges();      
  }

}
