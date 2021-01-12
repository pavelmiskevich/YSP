import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DoCheck, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Category } from "../model/category.model";
import { CategoryRepository } from "../model/category.repository";
import { Router } from "@angular/router";
import { Summary } from "../model/summary.model";

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html'
  ,changeDetection: ChangeDetectionStrategy.OnPush
  //styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit, DoCheck, OnChanges {

  public isAuth = true;
  public perPage = 4;
  public selectedPage = 1;

  constructor(private cdr: ChangeDetectorRef, private repository: CategoryRepository, private summary: Summary) { 
      //this.cdr.detach();
  }

  ngOnInit() {
    setTimeout(() => {
      this.summary.itemCount = this.categories.length; //this.repository.getCategories().length;
      //this.cdr.markForCheck();
      this.cdr.detectChanges();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('OnChanges CategoryComponent');
  }

  ngDoCheck(): void {
    //console.log('DoCheck CategoryComponent');
    this.cdr.detectChanges();
  }

  get categories(): Category[] {
      //let pageIndex = (this.selectedPage - 1) * this.sitesPerPage;
      return this.repository.getCategories(); //.sort();      
          //.slice(pageIndex, pageIndex + this.sitesPerPage);
  }

  // deactivate(entity: Category) {
  //   this.repository.deactivate(entity)
  // }

  // delete(id: number) {
  //   this.repository.delete(id);
  // }

  changePage(newPage: number) {
    this.selectedPage = newPage;    
  }

  changePageSize(newSize: number) {
      this.perPage = Number(newSize);
      this.changePage(1);
  }

  get pageCount(): number {
      return Math.ceil(this.repository.getCategories().length / this.perPage);
  }
}
