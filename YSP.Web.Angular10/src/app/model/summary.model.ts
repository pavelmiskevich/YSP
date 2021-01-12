import { Injectable } from "@angular/core"; 

//Декоратор @Injectable, который применяется к классу Cart в листинге, означает, что класс будет использоваться как служба
@Injectable() 
export class Summary {     
    public itemCount: number = 0; 
    public name?: string = null; 
}