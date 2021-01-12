import { Injectable } from "@angular/core";
//https://metanit.com/web/angular2/6.5.php
//https://angular.io/tutorial/toh-pt6
//import { Http, Request, RequestMethod } from "@angular/http";
import { HttpClient, HttpHeaders } from '@angular/common/http';
//import { Observable } from "rxjs/Observable";
import { Observable } from "rxjs";
import { Category } from "./category.model";
import { Site } from "./site.model";
//import "rxjs/src/operators/map";
//import "rxjs/operators/map";
import {catchError, map, tap} from 'rxjs/operators';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { IEntity } from './entity';
import { environment } from '../../environments/environment';
import { Region } from './region.model';
import { Query } from './query.model';
//import * as _ from 'lodash';
//const PROTOCOL = "https";
const PROTOCOL = "http";
//const PORT = 5001;
const PORT = 8080;

@Injectable()
export class RestDataSource {
    baseUrl: string;
    auth_token: string;
    status: string;
    errorMessage: string;    

    constructor(private http: HttpClient) {
        //this.baseUrl = `${PROTOCOL}://${location.hostname}:${PORT}/`;
        this.baseUrl = environment.apiEndpoint;
    }
    
    authenticate(user: string, pass: string): Observable<boolean> {
        return this.http.post(
            this.baseUrl + "login", 
            { name: user, password: pass }
        )
        .pipe(
            map( response => {
                let r = JSON.parse(JSON.stringify(response));                
                this.auth_token = r.success ? r.token : null;
                return r.success;
              })
        );        
        // return this.http.request(new Request({
        //     method: RequestMethod.Post,
        //     url: this.baseUrl + "login",
        //     body: { name: user, password: pass }
        // })).map(response => {
        //     let r = response.json();
        //     this.auth_token = r.success ? r.token : null;
        //     return r.success;
        // });
    }

    getCategories(): Observable<Category[]> {
        // const myHeaders = new HttpHeaders().set('Authorization', 'my-auth-token');          
        // return this.http.post('http://localhost:3000/postuser', user, {headers:myHeaders}); 
        //return this.http.post('http://localhost:3000/postuser', body); 

        //if (auth && this.auth_token != null) {
        if (this.auth_token != null) {
            return this.http.get<Category[]>(this.baseUrl + "Categories", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            return this.http.get<Category[]>(this.baseUrl + "Categories", {headers: new HttpHeaders().set("Access-Control-Allow-Headers", "X-Requested-With, content-type")});
        }        
        //return this.sendRequest((RequestMethod.Get, "products");
    }
    
    getRegions(): Observable<Region[]> {        
        if (this.auth_token != null) {
            return this.http.get<Region[]>(this.baseUrl + "Regions", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            return this.http.get<Region[]>(this.baseUrl + "Regions", {headers: new HttpHeaders().set("Access-Control-Allow-Headers", "X-Requested-With, content-type")});
        }
    }    

    getSites(): Observable<Site[]> {
        // const myHeaders = new HttpHeaders().set('Authorization', 'my-auth-token');          
        // return this.http.post('http://localhost:3000/postuser', user, {headers:myHeaders}); 
        //return this.http.post('http://localhost:3000/postuser', body); 

        //if (auth && this.auth_token != null) {
        if (this.auth_token != null) {
            return this.http.get<Site[]>(this.baseUrl + "Sites", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            return this.http.get<Site[]>(this.baseUrl + "Sites");
        }        
        //return this.sendRequest((RequestMethod.Get, "products");
    }

    // function isCategory(arg: any): arg is Category;
    // function isSite(arg: any): arg is Site;
    
    //TODO: перевести на универсальный метод
    // deactivateEntity(entity: Category | Site, entityName: string) {
    deactivateEntity(entity: Category | Site | Query, entityName: string) {    
        if (this.auth_token != null) {
            //this.http.get<Site[]>(this.baseUrl + "api/v1/Sites", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            //entity = new Category();
            // if (entity instanceof Category) {
            //     //var site: Site = entity;
            // }

            // if (isCategory(entity)) {
            //     let s: any = entity;
            // }
            // if (isSite(entity)) {
            //     let s: any = entity;
            // }

            // else {
            //     return;
            // }
            //switch(true) { 

            // const instance = new Category();

            // console.log(instance.constructor.name); // MyClass
            // console.log(entity.constructor.name); // MyClass
            // console.log(entity.type); // MyClass
            // console.log((entity as Category).type); // MyClass
            // console.log(typeof (entity));
            // console.log(entity.getName());
            // console.log((entity as Category).getName());
            // console.log(Category.name);              // MyClass

            switch (entity.type) { 
                //case (entity instanceof Site): {
                case 'Sites': {
                    (entity as Site).regionId = (entity as Site).region.id;
                    (entity as Site).categoryId = (entity as Site).category.id;
                    (entity as Site).userId = (entity as Site).user.id;            
                    if((entity as Site).name == '') (entity as Site).name = (entity as Site).url;
                    break; 
                } 
                case 'Categories': {
                    (entity as Category).parentId = (entity as Category).parent.id;
                    break; 
                } 
                case 'Queries': {
                    (entity as Query).siteId = (entity as Query).site.id;
                    break; 
                } 
                default: { 
                   console.log("Invalid entity type " + entity); 
                   break;              
                } 
             }

            // if (entity instanceof Site) {
            //     entity.regionId = entity.region.id;
            //     entity.categoryId = entity.category.id;
            //     entity.userId = entity.user.id;            
            //     if(entity.name == '') entity.name = entity.url;
            // }

            // entity.regionId = entity.region.id;
            // entity.categoryId = entity.category.id;
            // entity.userId = entity.user.id;            
            // if(entity.name == '') entity.name = entity.url;

            if(isSite(entity)) {
                entity.regionId = entity.region.id;
                entity.categoryId = entity.category.id;
                entity.userId = entity.user.id;            
                if(entity.name == '') entity.name = entity.url;
            }
            else {
                if(isQuery(entity)) {
                    entity.siteId = entity.site.id;                    
                }
                else {
                    entity.parentId = entity.parent?.id;
                }
                //entity.parentId = entity.parent?.id;
            }
            
            if(entity.isActive) {
                entity.isActive = false
            }
            else
            {
                entity.isActive = true;
            }
            
            this.http.put(this.baseUrl + entityName + "/" + entity.id, entity)
            .subscribe({
                next: data => {
                    this.status = 'Deactivate successful';
                },
                error: error => {
                    this.errorMessage = error.message;
                    console.error('There was an error!', error);
                }
            });
        }        
        //return this.sendRequest((RequestMethod.Get, "products");
    }

    deactivateQuery(entity: Query) {    
        if (this.auth_token != null) {
            //this.http.get<Site[]>(this.baseUrl + "api/v1/Sites", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {            
            entity.siteId = entity.site.id;
            if(entity.isActive) {
                entity.isActive = false
            }
            else
            {
                entity.isActive = true;
            }
            
            this.http.put(this.baseUrl + "Queries" + "/" + entity.id, entity)
            .subscribe({
                next: data => {
                    this.status = 'Deactivate successful';
                },
                error: error => {
                    this.errorMessage = error.message;
                    console.error('There was an error!', error);
                }
            });
        }        
        //return this.sendRequest((RequestMethod.Get, "products");
    }

    deleteSite(siteId: number) {        
        if (this.auth_token != null) {
            this.http.get<Site[]>(this.baseUrl + "Sites", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            this.http.delete(this.baseUrl + "Sites/" + siteId)
            .subscribe({
                next: data => {
                    this.status = 'Delete successful';
                },
                error: error => {
                    this.errorMessage = error.message;
                    console.error('There was an error!', error);
                }
            });
        }        
        //return this.sendRequest((RequestMethod.Get, "products");
    }

    deleteEntity(id: number, entityName: string) {        
        if (this.auth_token != null) {
            //this.http.get<Site[]>(this.baseUrl + "api/v1/Sites", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            this.http.delete(this.baseUrl + entityName + "/" + id)
            .subscribe({
                next: data => {
                    this.status = 'Delete successful';
                },
                error: error => {
                    this.errorMessage = error.message;
                    console.error('There was an error!', error);
                }
            });
        }        
        //return this.sendRequest((RequestMethod.Get, "products");
    }

    getQueries(): Observable<Query[]> {
        // const myHeaders = new HttpHeaders().set('Authorization', 'my-auth-token');          
        // return this.http.post('http://localhost:3000/postuser', user, {headers:myHeaders}); 
        //return this.http.post('http://localhost:3000/postuser', body); 

        //if (auth && this.auth_token != null) {
        if (this.auth_token != null) {
            return this.http.get<Query[]>(this.baseUrl + "Queries", {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            return this.http.get<Query[]>(this.baseUrl + "Queries");
        }        
        //return this.sendRequest((RequestMethod.Get, "products");
    }

    // createQuery(query: string): Observable<Query> {
    //     console.log(JSON.stringify(query));
    //     if (this.auth_token != null) {
    //         return this.http.post<Query>(this.baseUrl + "api/v1/Queries", query, {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
    //     }
    //     else {
    //         return this.http.post<Query>(this.baseUrl + "api/v1/Queries", query);
    //     } 
    // }

    createCategory(category: Category): Observable<Category> {
        console.log(JSON.stringify(category));
        if (this.auth_token != null) {
            return this.http.post<Category>(this.baseUrl + "Categories", category, {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            return this.http.post<Category>(this.baseUrl + "Categories", category)
            .pipe(
                tap( // Log the result or error
                  data => this.status = 'Create successful',
                  error => {
                    this.errorMessage = error.message;
                    console.error('There was an error!', error);
                  })
                );
            // .pipe(
            //     catchError(this.handleError('addHero', query))
            // );
        } 
    }

    createSite(site: Site): Observable<Site> {
        console.log(JSON.stringify(site));
        if (this.auth_token != null) {
            return this.http.post<Site>(this.baseUrl + "Sites", site, {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            return this.http.post<Site>(this.baseUrl + "Sites", site)
            .pipe(
                tap( // Log the result or error
                  data => this.status = 'Create successful',
                  error => {
                    this.errorMessage = error.message;
                    console.error('There was an error!', error);
                  })
                );
            // .pipe(
            //     catchError(this.handleError('addHero', query))
            // );
        } 
    }

    createQuery(query: Query): Observable<Query> {
        console.log(JSON.stringify(query));
        if (this.auth_token != null) {
            return this.http.post<Query>(this.baseUrl + "Queries", query, {headers: new HttpHeaders().set("Authorization", `Bearer<${this.auth_token}>`)} );
        }
        else {
            return this.http.post<Query>(this.baseUrl + "Queries", query)
            .pipe(
                tap( // Log the result or error
                  data => this.status = 'Create successful',
                  error => {
                    this.errorMessage = error.message;
                    console.error('There was an error!', error);
                  })
                );
            // .pipe(
            //     catchError(this.handleError('addHero', query))
            // );
        } 
    }
}

function isCategory(object: any): object is Category {
    return true;
}
function isQuery(object: any): object is Query {
    return object.lastCheck !== undefined;
}
function isSite(object: any): object is Site {
    return object.url !== undefined;
}