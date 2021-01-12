import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Site } from "./site.model";
import { Query } from "./query.model";
import { StaticDataSource } from "./static.datasource";
import { tap } from 'rxjs/operators';
@Injectable()
export class QueryRepository {
    private sites: Site[] = [];
    public queries: Query[] = [];
    constructor(private dataSource: StaticDataSource) {
        dataSource.getQueries().subscribe(data => {
            this.queries = data.sort((a, b) => (b.id - a.id));
            this.sites = data.map(p => p.site)
            .filter(
                (thing, i, arr) => arr.findIndex(t => t.name === thing.name) === i
              );
                //.filter((c, index, array) => array.indexOf(c) == index).sort();
        });
    }
    getQueries(siteId: number = null): Query[] {
      let index = this.queries.filter(p => siteId == null || siteId == p.site.id).length;
        return this.queries
            .filter(p => siteId == null || siteId == p.site.id);
            //.filter(p => siteId == null || siteId == p.site?.id);
    }
    getQuery(id: number): Query {
        return this.queries.find(p => p.id == id);
    }
    getSites(): Site[] {
        return this.sites;
    }
    // deactivateQuery(entity: Query) {
    //     this.dataSource.deactivateEntity(entity, "Queries");
    // }
    // deleteQuery(queryId: number) {
    //     this.dataSource.deleteQuery(queryId);
    // }
    // createQuery(query: string): Observable<Query> {
    //     return this.dataSource.createQuery(query);
    // }

    //TODO: переделать на async/await
    //https://webformyself.com/ispolzovanie-funkcii-async-await-v-angular/
    createQuery(query: Query): Observable<Query> {
        let newQuery: Query;
        let obNewQuery: Observable<Query>;
        
        //this.queries.push(newQuery);
        //this.queries.push({id: newQuery.id, name: newQuery.name});
        //this.queries.push(new Query(newQuery.id, newQuery.name));
        //this.queries.push({id: 888, name: query.name, siteId: 18});

        // this.dataSource.createQuery(query).subscribe( value => {        
        //     newQuery = value;
        //     this.queries.push(newQuery);  
        // });
        obNewQuery = this.dataSource.createQuery(query)
        .pipe(
            tap( // Log the result or error
              value => {
                newQuery = value;
                newQuery.site = new Site();
                newQuery.site.id = query.siteId;
                let start = this.queries.length;
                this.queries.push(newQuery);
                let end = this.queries.length;
              },
              error => {
                console.error('There was an error!', error);
              })
            );
        // obNewQuery.subscribe( value => {        
        //         newQuery = value;
        //         newQuery.site = new Site();
        //         newQuery.site.id = query.siteId;
        //         this.queries.push(newQuery);  
        //     });
        return obNewQuery;
        //return newQuery;
    }

    deactivate(entity: Query) {
        entity.constructor();        
        this.dataSource.deactivateEntity(entity, "Queries");
    }
    delete(id: number) {
        this.dataSource.deleteEntity(id, "Queries");
    }
}