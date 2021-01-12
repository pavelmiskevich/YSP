import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryComponent } from './category/category.component';
import { SiteComponent } from './site/site.component';
import { QueryComponent } from './query/query.component';

const routes: Routes = [
  { path: '', redirectTo: '/category', pathMatch: 'full' },
  { path: 'category', component: CategoryComponent },
  { path: 'site', component: SiteComponent },  
  { path: 'site/:categoryId', component: SiteComponent },
  //{ path: 'query', component: QueryComponent },
  { path: 'query/:siteId', component: QueryComponent }
  //{ path: 'query', component: QueryComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
