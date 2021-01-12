import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Site } from "../model/site.model";
import { Category } from "../model/category.model";
import { SiteRepository } from "../model/site.repository";
import { ActivatedRoute, Router } from "@angular/router";
import { Summary } from "../model/summary.model";
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-site',
  templateUrl: './site.component.html'
  //,styleUrls: ['./site.component.css']
})
export class SiteComponent implements OnInit {
  categoryId: number;
  private subscription: Subscription;
  public selectedCategory = null;
  public isAuth = true;
  public sitesPerPage = 4;
  public selectedPage = 1;

  constructor(private activateRoute: ActivatedRoute, private repository: SiteRepository, private summary: Summary) { 
      this.subscription = activateRoute.params.subscribe(params=>this.categoryId=this.selectedCategory=params['categoryId']);
  }  

  //https://indepth.dev/everything-you-need-to-know-about-the-expressionchangedafterithasbeencheckederror-error/
  ngOnInit() {
    setTimeout(() => {
      this.summary.itemCount = this.repository.getSites(this.selectedCategory).length;
    }, 1000);
  }

  get sites(): Site[] {
      //this.summary.itemCount = this.repository.getSites(this.selectedCategory).length;
      let pageIndex = (this.selectedPage - 1) * this.sitesPerPage
      return this.repository.getSites(this.selectedCategory).sort()
          .slice(pageIndex, pageIndex + this.sitesPerPage);
  }

  deactivateSite(entity: Site) {
    this.repository.deactivateSite(entity)
  }

  deleteSite(siteId: number) {
    this.repository.deleteSite(siteId)
  }

  get categories(): Category[] {
      return this.repository.getCategories();
  } 

  //changeCategory(newCategoryId?: number) {
  changeCategory(newCategory?: Category) {
    this.summary.itemCount = newCategory ? this.repository.getSites(newCategory.id).length 
        : this.repository.getSites().length;
    this.summary.name = newCategory ? newCategory.name : ''; //String(newCategoryId);
    this.selectedCategory = newCategory?.id;
}

  changePage(newPage: number) {
    this.selectedPage = newPage;    
  }
  changePageSize(newSize: number) {
      this.sitesPerPage = Number(newSize);
      this.changePage(1);
  }

  get pageCount(): number {
      return Math.ceil(this.repository.getSites(this.selectedCategory).length / this.sitesPerPage);
  }
}
