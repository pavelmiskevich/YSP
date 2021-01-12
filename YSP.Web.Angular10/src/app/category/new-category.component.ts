import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from '../model/category.model';
import { CategoryRepository } from '../model/category.repository';
import { Summary } from '../model/summary.model';

@Component({
  selector: 'app-new-category',
  templateUrl: './new-category.component.html'
  //,styleUrls: ['./new-category.component.css']
  ,styleUrls: ['../app.component.css']
  ,changeDetection: ChangeDetectionStrategy.OnPush  
})
export class NewCategoryComponent implements OnInit {  
  private subscription: Subscription;
  
  newCategory: boolean = false;
  submitted: boolean = false;

  @Input() categories;

  constructor(private cdr: ChangeDetectorRef, private repository: CategoryRepository, 
    private summary: Summary, public category: Category) { 
      this.category.parentId = null;
      //this.cdr.detach();
  }

  ngOnInit(): void {
  }

  submit(form: NgForm) {
      this.submitted = true;
      if (form.valid) {          
          this.repository.createCategory(this.category)
          .toPromise()
            .then(
                res => { // Success
                  this.newCategory = true;
                  this.submitted = false;
                  //this.query.site.id = this.query.siteId;
                  //this.categories.push(this.category);
                  this.summary.itemCount = this.repository.categories.length;
                  //this.cdr.detectChanges();
                },
                error => { // Error
                  console.error('There was an error!', error);
                }
            );
          //this.cdr.detectChanges();
      }
    }

  // add() {    
  //     let newC = new Category();
  //     //newC.id = 777;
  //     newC.name = 'testCategory';
  //     //public categories on repository
  //     this.repository.categories.push(newC);
  //     //this.repository.createQuery(newQ).subscribe();
  //     this.summary.itemCount = this.repository.categories.length;
  //     this.cdr.detectChanges();
  // }

}
