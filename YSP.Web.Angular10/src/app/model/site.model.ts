import { Injectable } from "@angular/core"; 
import { Base } from "./base.model";
import { Category } from "./category.model";
import { User } from "./user.model";
import { Region } from "./region.model";
import { IEntity } from './entity';

//TODO: доедлать наследование
//http://typescript-lang.ru/docs/Classes.html
//export class Site extends Base {
@Injectable() 
export class Site implements IEntity {
    public type: 'Sites';
    public id: number;
    public url: string;
    public regionId: number;
    public region: Region;
    public name?: string;  
    public descr?: string;
    public lastCheck?: string;
    public categoryId?: number;
    public category?: Category;
    public userId?: number;
    public user?: User;
    public addDate?: string;
    public isActive?: boolean;    
    // constructor(region: Region, category?: Category, User?: User );
    
    constructor(){
        this.type = 'Sites';
    }
    //constructor(options?: Site) { 
        //https://stackblitz.com/edit/typescript-constructor-comparison
        //Object.assign(this, options);
    //}


    // constructor(id: number, url: string, regionId: number, public region: Region, 
    //     name?: string, descr?: string, lastCheck?: string, categoryId?: number, 
    //     public category?: Category, userId?: number, public User?: User, addDate?: string,  isActive?: boolean) {
    //         //super(id, addDate, isActive);
    //      }

    getName() : string {
        let comp:any = this.constructor;
        return comp.name;
    }
}