import { Injectable } from "@angular/core";
import { Category } from "./category.model";
import { Site } from "./site.model";
import { Query } from "./query.model";
//import { Observable } from "rxjs/Observable";
//import "rxjs/add/observable/from";
import { Observable } from "rxjs";
import { from } from "rxjs";
import { from as observableFrom } from 'rxjs';
//import { Order } from "./order.model";

import { IEntity } from './entity';
import { Region } from './region.model';

// import { HttpClient, HttpHeaders } from '@angular/common/http';
// const PROTOCOL = "http";
// const PORT = 3500;

@Injectable()
export class StaticDataSource {
    // baseUrl: string;
    // constructor(private http: HttpClient) {
    //     this.baseUrl = `${PROTOCOL}://${location.hostname}:${PORT}/`;
    // }
    private categories: Category[] = [
        // new Category(1, "Category 1", null, null, null),
        // new Category(2, "Category 2", null, "2020-09-10", true),
        // new Category(3, "Category 3", 1, null, true),
        // new Category(4, "Category 4", 1, null, true),
        // new Category(5, "Category 5", 3, null, true),
        // new Category(6, "Category 6", 4, null, true),
        // new Category(7, "Category 7", null, null, true),
        // new Category(8, "Category 8", null, null, true),
        // new Category(9, "Category 9", null, null, true),
        // new Category(10, "Category 10", null, null, false),
        // new Category(11, "Category 11", null, null, true),
        // new Category(12, "Category 12", null, null, true),
        // new Category(13, "Category 13", null, null, true),
        // new Category(14, "Category 14", null, null, true),
        // new Category(15, "Category 15", null, null , true)
    ];   

    getCategories(): Observable<Category[]> {
        //https://stackoverflow.com/questions/50186371/property-from-does-not-exist-on-type-typeof-observable-angular-6        
        //return Observable.from([this.products]);

        // let str2 = `${PROTOCOL}://${location.hostname}:${PORT}/`;
        // let str = str2 + "products";
        // return this.http.get<Product[]>(this.baseUrl + "products");

        return from([this.categories]);

        //return observableFrom([this.products]);
    }

    createCategory(category: Category): Observable<Category> {
        console.log(JSON.stringify(category));
        return from([category]);
    }

    createSite(site: Site): Observable<Site> {
        console.log(JSON.stringify(site));
        return from([site]);
    }

    private regions: Region[] = [];
    getRegions(): Observable<Region[]> {        
        return from([this.regions]);
    }

    private sites: Site[] = [];
    getSites(): Observable<Site[]> {        
        return from([this.sites]);
    }

    private queries: Query[] = [];
    getQueries(): Observable<Query[]> {        
        return from([this.queries]);
    }
    // createQuery(query: string) {
    //     console.log(JSON.stringify(query));        
    // }
    createQuery(query: Query): Observable<Query> {
        console.log(JSON.stringify(query));
        return from([query]);
    }

    deactivateEntity(entity: IEntity, entityName: string) { }
    deleteSite(siteId: number) { }
    deleteEntity(id: number, entityName: string) {}

    // saveOrder(order: Order): Observable<Order> {
    //     console.log(JSON.stringify(order));
    //     return from([order]);
    // }
}