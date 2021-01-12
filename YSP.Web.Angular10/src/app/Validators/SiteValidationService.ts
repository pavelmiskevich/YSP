import { Injectable } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
@Injectable()
export class SiteValidationService {
 private users: string[];
 constructor() {
  /** Пользователи, зарегистрированные в системе */
  this.users = ['john', 'ivan', 'anna'];
 }

 /** Запрос валидации */
 validateName(userName: string): Observable<ValidationErrors> {
  /** Эмуляция запроса на сервер */
  return new Observable<ValidationErrors>(observer => {
   const user = this.users.find(user => user === userName);
   /** если пользователь есть в массиве, то возвращаем ошибку */
   if (user) {
    observer.next({
     nameError: 'Пользователь с таким именем уже существует'
    });
     observer.complete();
    }

    /** Если пользователя нет, то валидация успешна */
    observer.next(null);
    observer.complete();
   }); //.delay(1000);
  }
}