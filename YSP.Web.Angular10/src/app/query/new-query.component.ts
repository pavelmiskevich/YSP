import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { ActivatedRoute} from '@angular/router';
import { QueryRepository } from "../model/query.repository";
import { Query } from "../model/query.model";
import { Subscription } from 'rxjs';
import { audit, tap } from 'rxjs/operators';

@Component({
  selector: 'app-new-query',
  templateUrl: './new-query.component.html'
  //,styleUrls: ['./new-query.component.css']
  ,styleUrls: ['../app.component.css']
  ,changeDetection: ChangeDetectionStrategy.OnPush  
})
export class NewQueryComponent implements OnInit {
  siteId: number;
  private subscription: Subscription;
  
  newQuery: boolean = false;
  submitted: boolean = false;
  //queryName: string = "test";
  //public query: Query;

  @Input() queries;

  constructor(private cdr: ChangeDetectorRef, private activateRoute: ActivatedRoute, 
    private repository: QueryRepository, private query: Query) {
    this.subscription = activateRoute.params.subscribe(params=>this.siteId=params['siteId']);
      // setTimeout(() => {
      //   this.query = new Query();
      // });
      //this.query = new Query();
   }

  ngOnInit(): void {
    setTimeout(() => {      
      //this.cdr.detectChanges();
    }, 1000);
  }

  submitQuery(form: NgForm) {
    this.submitted = true;
    if (form.valid) {
        this.query.siteId = this.siteId; //String(this.siteId);
        //this.query.site.id = this.siteId;
        this.repository.createQuery(this.query)
        .toPromise()
          .then(
              res => { // Success
                this.newQuery = true;
                this.submitted = false;
                this.query.site.id = this.query.siteId;
                this.queries.push(this.query);
                //this.ngOnInit();
                this.cdr.detectChanges();
              },
              error => { // Error
                console.error('There was an error!', error);
              }
          );
    }
  }
}
