import { Injectable } from "@angular/core"; 

//TODO: доделать модель пользователя
@Injectable() 
export class User {
    //public id: number;
    //public name: string;
    constructor(public id: number, public name: string) { }
}