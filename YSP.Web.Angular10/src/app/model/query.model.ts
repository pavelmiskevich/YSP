import { Injectable, OnInit } from "@angular/core"; 
import { Base } from "./base.model";
import { IEntity } from './entity';
import { Site } from "./site.model";

// @Injectable({
//   providedIn: 'root'
// })

//TODO: доедлать наследование
//http://typescript-lang.ru/docs/Classes.html
//export class Site extends Base {    

@Injectable() 
export class Query implements OnInit, IEntity {
    public type: string;
    public id?: number;
    public name: string;
    public siteId: number;
    //public site: Site;    
    public lastCheck?: string;
    public addDate?: string;
    public isActive?: boolean;
    constructor(public site?: Site) { 
        this.type = 'Queries';
    } //public site: Site

    ngOnInit(): void {
        this.type = this.constructor.name; 
    }    
    getName(): string {
        let comp:any = this.constructor;
        return comp.name;
    }
    //constructor(public query: Query) { }
    
    // clear() {
    //     this.id = null;
    //     this.name = this.address = this.city = null;
    //     this.state = this.zip = this.country = null;
    //     this.shipped = false;
    //     this.cart.clear();
    // }
}