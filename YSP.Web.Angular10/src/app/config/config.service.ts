import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
//https://angular.io/guide/http
@Injectable()
export class ConfigService {
  constructor(private http: HttpClient) { }
}