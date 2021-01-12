import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Category } from "./category.model";
import { StaticDataSource } from "./static.datasource";
@Injectable()
export class CategoryRepository {
    public categories: Category[] = [];
    constructor(private dataSource: StaticDataSource) {
        dataSource.getCategories().subscribe(data => {
            this.categories = data; //.reverse(); //.sort((a, b) => (b.id - a.id));            
        });
    }
    getCategoriesByParentId(parentId: number = null): Category[] {
        return this.categories
            .filter(p => parentId == null || parentId == p.parentId);
    }
    getCategory(id: number): Category {
        return this.categories.find(p => p.id == id);
    }
    getCategories(): Category[] {
        return this.categories;
    }

    createCategory(category: Category): Observable<Category> {
        let newCategory: Category;
        let obNewCategory: Observable<Category>;        
        
        obNewCategory = this.dataSource.createCategory(category)
        .pipe(
            tap( // Log the result or error
              value => {
                newCategory = value;
                this.categories.push(newCategory);
              },
              error => {
                console.error('There was an error!', error);
              })
            );
        // obNewQuery.subscribe( value => {        
        //         newQuery = value;
        //         newQuery.site = new Site();
        //         newQuery.site.id = query.siteId;
        //         this.queries.push(newQuery);  
        //     });
        return obNewCategory;
    }

    deactivate(entity: Category) {
        entity.constructor();        
        this.dataSource.deactivateEntity(entity, "Categories");
    }
    delete(id: number) {
        this.dataSource.deleteEntity(id, "Categories");
    }
}