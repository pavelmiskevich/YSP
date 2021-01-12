import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Category } from '../model/category.model';
import { CategoryRepository } from '../model/category.repository';
import { Region } from '../model/region.model';
import { RegionRepository } from '../model/region.repository';
import { Site } from '../model/site.model';
import { SiteRepository } from '../model/site.repository';
import { Summary } from '../model/summary.model';

@Component({
  selector: 'app-new-site',
  templateUrl: './new-site.component.html'
  //,styleUrls: ['./new-site.component.css']
  ,styleUrls: ['../app.component.css']
})
export class NewSiteComponent implements OnInit {
      newSiteForm: FormGroup;
      newSite: boolean = false;
      private sites2: string[];

  constructor(private fb: FormBuilder, private categoryRepository: CategoryRepository, 
    private regionRepository: RegionRepository, private siteRepository: SiteRepository, private summary: Summary) { 
      this._createForm();

      this.sites2 = ['ya.ru', 'ya1.ru'];

      // this.newSiteForm.valueChanges.subscribe(v => {
      //   console.log(v);
      // });
  }

  ngOnInit(): void {
      // setTimeout(() => {
      //   this._createForm();
      // });
  }

  get f(){
    return this.newSiteForm.controls;
  }   

  get categories(): Category[] {
      //let pageIndex = (this.selectedPage - 1) * this.sitesPerPage;
      return this.categoryRepository.getCategories(); //.sort();      
          //.slice(pageIndex, pageIndex + this.sitesPerPage);
  }

  get regions(): Region[] {    
      return this.regionRepository.getRegions();  
  }

  get sites(): Site[] {    
    return this.siteRepository.getSites();
  }

  // get _url(){
  //   return this.newSiteForm.get('url');
  // }  

  private _createForm(){
    const reg = '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';
    this.newSiteForm = this.fb.group({
      id: [{ value: null, disabled: true }],
      url: ['', [Validators.required, Validators.min(4), Validators.pattern(reg), this.urlAsyncValidator.bind(this) ]], //this.urlValidator //[this.urlAsyncValidator.bind(this)]
      name: '',
      descr: '',
      categoryId: ['', [Validators.required]],
      regionId: ['', [Validators.required]]

      // ,passengerContacts: this.fb.group({
      //   telegram: '',
      //   whatsapp: ''
      // })
    });

    // this.newSiteForm = new FormGroup({
    //   id: new FormControl({value: null, disabled: true}),
    //   //url: ['', [Validators.required, Validators.min(4), Validators.pattern(/^[0-9]+(?!.)/)]],
    //   url: new FormControl(null),
    //   name: new FormControl(null),      
    //   descr: new FormControl(null),
    //   category: new FormControl(null),
    //   //region: ['', [Validators.required]],
    //   region: new FormControl(null),
     
    //   passengerContacts: new FormGroup({
    //     telegram: new FormControl(null),
    //     whatsapp: new FormControl(null)
    //   })
    // });
  }

  submit(){
      console.log(this.newSiteForm.value)
      if (this.newSiteForm.valid) {          
        this.siteRepository.createSite(this.newSiteForm.value)
        .toPromise()
          .then(
              res => { // Success
                this.newSite = true;
                this.summary.itemCount = this.siteRepository.sites.length;
                //this.cdr.detectChanges();
              },
              error => { // Error
                console.error('There was an error!', error);
              }
          );
        //this.cdr.detectChanges();
    }
  }

  clear(){
      console.log('clear ' + this.newSiteForm.value)

      //this.newSiteForm.setValue({
      this.newSiteForm.patchValue({
        url: '', 
        name: '',
        descr: '',
        category: '',
        region: ''
      });
  }

  //https://habr.com/ru/post/347126/
  urlAsyncValidator(control: FormControl): Observable<ValidationErrors> {
      return this.validateURL(control.value);
   }

  validateURL(urlName: string): Observable<ValidationErrors> {
      /** Эмуляция запроса на сервер */
      return new Observable<ValidationErrors>(observer => {
        const urlV = this.sites.find(site => site.url === urlName);
        if (urlV) {
        observer.next({
          nameError: 'Такой URL уже существует'
        });
          observer.complete();
        }

        /** Если URL нет, то валидация успешна */
        observer.next(null);
        observer.complete();
        }); //.delay(1000);
  }

  // urlValidator(control: FormControl): ValidationErrors {
  //     //const url = this.sites.find(site => site.url === control.value);
  //     //var url = this.regions.find(site => site.name === control.value);
  //     //const url = this.siteRepository.getSites().find(site => site.url === control.value);
  //     const url = this.sites2.find(sites2 => sites2 === control.value);
      
  //     /** если URL есть в массиве, то возвращаем ошибку */
  //     if (url) {
  //     //if(control.value==="ya1.ru"){
  //         return { existUrl: 'Такой URL уже есть'};
  //     }
  //     return null;
  // }

  // urlValidator(control: FormControl): {[s:string]:boolean}{         
  //     if(control.value==="ya1.ru"){
  //         return {"url": true};
  //     }
  //     return null;
  // }
}
