import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Query } from '../model/query.model';
import { QueryRepository } from '../model/query.repository';
import { Summary } from '../model/summary.model';

@Component({
  selector: 'app-query'
  ,templateUrl: './query-item.component.html'
  //template: `{{query.name}} {{runChangeDetection}}`,
  ,changeDetection: ChangeDetectionStrategy.OnPush
})
export class QueryItemComponent {
  @Input() query;
  private subscription: Subscription;
  public isAuth = true;
  public selectedSite = null;

  constructor(private activateRoute: ActivatedRoute, private cdr: ChangeDetectorRef, 
    private repository: QueryRepository, private summary: Summary) { 
      this.subscription = activateRoute.params.subscribe(params=>this.selectedSite=params['siteId']);
    }

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

  // get runChangeDetection() {
  //   console.log('QueryItemComponent - Checking the view');
  //   return true;
  // }
}