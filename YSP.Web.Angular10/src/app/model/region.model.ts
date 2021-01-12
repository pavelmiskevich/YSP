import { Injectable } from "@angular/core"; 

@Injectable() 
export class Region {
    // public id: number;
    // public name: string;
    constructor(public id: number, public name: string) { }
}