import { Injectable, OnInit } from "@angular/core"; 
import { IEntity } from './entity';

@Injectable() 
export class Category implements OnInit, IEntity {
    public type: string; //this.constructor.name;
    public id?: number;
    public name?: string;   
    public parentId?: number;
    public parent?: Category;
    //public parentCategory?: Category;
    public addDate?: string;
    public isActive?: boolean;

    // constructor();
    // constructor(id?: number, name?: string, parentId?: number, addDate?: string, isActive?: boolean);

    //constructor(type?: 'Categories', id?: number, name?: string, parentId?: number, parent?: Category, addDate?: string, isActive?: boolean, options?: Category) {
    constructor(){
        this.type = 'Categories';
    //constructor(options?: Category) { 
        //https://stackblitz.com/edit/typescript-constructor-comparison
        //Object.assign(this, options);
    }

    ngOnInit(){
        this.type = this.constructor.name; 
     }

    getName() : string {
        let comp:any = this.constructor;
        return comp.name;
    }
}