import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Category } from "./category.model";
import { Site } from "./site.model";
import { StaticDataSource } from "./static.datasource";
@Injectable()
export class SiteRepository {
    public sites: Site[] = [];
    private categories: Category[] = [];
    constructor(private dataSource: StaticDataSource) {
        dataSource.getSites().subscribe(data => {
            //this.sites = data;
            this.sites = data.sort((a, b) => (b.id - a.id));
            this.categories = data.map(p => p.category)
            .filter(
                (thing, i, arr) => arr.findIndex(t => t.name === thing.name) === i
              );
                //.filter((c, index, array) => array.indexOf(c) == index).sort();
        });
    }
    getSites(categoryId: number = null): Site[] {
        return this.sites
            .filter(p => categoryId == null || categoryId == p.category.id);
    }
    getSite(id: number): Site {
        return this.sites.find(p => p.id == id);
    }
    getCategories(): Category[] {
        return this.categories;
    }
    createSite(site: Site): Observable<Site> {
        let newSite: Site;
        let obNewSite: Observable<Site>;
        
        obNewSite = this.dataSource.createSite(site)
        .pipe(
            tap( // Log the result or error
              value => {
                newSite = value;
                this.sites.push(newSite);
              },
              error => {
                console.error('There was an error createSIte!', error);
              })
            );
        // obNewQuery.subscribe( value => {        
        //         newQuery = value;
        //         newQuery.site = new Site();
        //         newQuery.site.id = query.siteId;
        //         this.queries.push(newQuery);  
        //     });
        return obNewSite;
    }
    deactivateSite(entity: Site) {
        this.dataSource.deactivateEntity(entity, "Sites");
    }
    deleteSite(siteId: number) {
        this.dataSource.deleteSite(siteId);
    }
}