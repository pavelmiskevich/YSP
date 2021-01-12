import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, SimpleChanges } from '@angular/core';
import { Query } from "../model/query.model";
import { Site } from "../model/site.model";
import { Summary } from "../model/summary.model";
import { QueryRepository } from "../model/query.repository";
import { ActivatedRoute} from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'query',
  templateUrl: './query.component.html'
  ,changeDetection: ChangeDetectionStrategy.OnPush
  //,styleUrls: ['./query.component.css']
})
export class QueryComponent implements OnInit {
  siteId: number;
  private subscription: Subscription;
  order: string = 'name';
  public selectedSite = null;
  public isAuth = true;
  public sitesPerPage = 4;
  public selectedPage = 1;    
  //_queries: Query[]; // = [{ name: 'One' }, { name: 'Two' }];

  constructor(private cdr: ChangeDetectorRef, private activateRoute: ActivatedRoute, private repository: QueryRepository, private summary: Summary) { 
      //this.id = activateRoute.snapshot.params['id'];      
      this.subscription = activateRoute.params.subscribe(params=>this.siteId=this.selectedSite=params['siteId']);
  }

  ngOnInit() {
    // this.route.queryParams.subscribe(params => {
    //   this.site = params['site'];
    // });
    setTimeout(() => {
      this.summary.itemCount = this.repository.getQueries(this.selectedSite).length;
      this.summary.name = '';
      //this._queries = this.queries;
      //this.cdr.markForCheck();
      //this.cdr.detectChanges();
    }, 1000);
  }

  get queries(): Query[] {
    //this.cdr.markForCheck();
    //this.summary.itemCount = this.repository.getSites(this.selectedCategory).length;
    let pageIndex = (this.selectedPage - 1) * this.sitesPerPage;
    let index = this.repository.getQueries(this.selectedSite).length;
    return this.repository.getQueries(this.selectedSite).sort() //;
        .slice(pageIndex, pageIndex + this.sitesPerPage);
  }

  // deactivate(entity: Query) {
  //   this.repository.deactivate(entity)
  // }

  // delete(id: number) {
  //   this.repository.delete(id)
  // }

  get sites(): Site[] {
      return this.repository.getSites();
  } 

  //changeCategory(newCategoryId?: number) {
  changeSite(newSite?: Site) {
    this.summary.itemCount = newSite ? this.repository.getQueries(newSite.id).length 
        : this.repository.getQueries().length;
    this.summary.name = newSite ? newSite.url : ''; //String(newQueryId);
    this.selectedSite = newSite?.id;
}

  changePage(newPage: number) {
    this.selectedPage = newPage;    
  }
  changePageSize(newSize: number) {
      this.sitesPerPage = Number(newSize);
      this.changePage(1);
  }

  get pageCount(): number {
      return Math.ceil(this.repository.getQueries(this.selectedSite).length / this.sitesPerPage);
  }

  add() {    
    let newQ = new Query();
    newQ.name = 'test';
    //newQ.site = new Site();
    newQ.siteId = 18;
    //newQ.site.id = 18;
    //this._queries.push(newQ);
    this.repository.createQuery(newQ).subscribe();
  }

  ngOnChanges(changes: SimpleChanges): void {
    //console.log('OnChanges CategoryItemComponent');    
  }

  ngDoCheck(): void {
    //console.log('DoCheck CategoryItemComponent');
    this.cdr.detectChanges();
  }

  deactivate(entity: Query) {
    this.repository.deactivate(entity);
    this.cdr.detectChanges();
  }

  //delete(id: number) {
  delete(entity: Query) {
      this.repository.delete(entity.id);
      //const index = this.repository.categories.indexOf(entity, 0);
      const index = this.repository.queries.indexOf(entity, 0);
      if (index > -1) {
        this.repository.queries.splice(index, 1);
      }
      this.summary.itemCount = this.repository.getQueries(this.selectedSite).length;
      this.cdr.detectChanges();      
  }

}
