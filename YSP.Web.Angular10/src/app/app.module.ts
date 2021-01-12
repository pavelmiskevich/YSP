import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

//import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CategoryComponent } from './category/category.component';
import { SiteComponent } from './site/site.component';
import { QueryComponent } from './query/query.component';
import { QueryListComponent } from './query/query-list.component';
import { QueryItemComponent } from './query/query-item.component';
import { PositionComponent } from './position/position.component';

import { CategoryRepository } from "./model/category.repository";
import { SiteRepository } from "./model/site.repository";
import { QueryRepository } from "./model/query.repository";
import { StaticDataSource } from "./model/static.datasource";
import { RestDataSource } from "./model/rest.datasource";

import { HttpClientModule } from '@angular/common/http';

import { CounterDirective } from "./counter.directive";
import { SummaryComponent } from './summary/summary.component';
import { Summary } from './model/summary.model';
import { Site } from './model/site.model';
import { Query } from './model/query.model';

import { FirstGuard } from "./first.guard";

import { AppRoutingModule } from './app-routing.module';
import { NewQueryComponent } from './query/new-query.component';
import { MenuComponent } from './menu/menu.component';
//https://github.com/VadimDez/ngx-order-pipe
import { OrderModule } from 'ngx-order-pipe';

import { ZoomDirective } from './directives/zoom.directive';
import { DuplicateContentDirective } from './directives/duplicate-content.directive';

import { CutTextPipe } from './pipes/cut-text.pipe';
import { Category } from './model/category.model';
import { DynamicListComponent } from './dynamiccomponents/dynamic-list.component';

import { SettingsHttpService } from './config/settings.http.service';
import { TodosComponent } from './todo.component';
import { CategoryListComponent } from './category/category-list.component';
import { CategoryItemComponent } from './category/category-item.component';
import { NewCategoryComponent } from './category/new-category.component';
import { SiteListComponent } from './site/site-list.component';
import { SiteItemComponent } from './site/site-item.component';
import { NewSiteComponent } from './site/new-site.component';
import { RegionRepository } from './model/region.repository';

export function app_Init(settingsHttpService: SettingsHttpService) {
  return () => settingsHttpService.initializeApp();
}

@NgModule({
  declarations: [
    AppComponent,
    CounterDirective,
    CategoryComponent,
    SiteComponent,
    QueryComponent,
    NewCategoryComponent,
    QueryListComponent,
    QueryItemComponent,
    PositionComponent,
    SummaryComponent,
    NewQueryComponent,
    MenuComponent,
    ZoomDirective,
    DuplicateContentDirective,
    CutTextPipe
    //,DynamicListComponent
    ,TodosComponent, CategoryListComponent, CategoryItemComponent, SiteListComponent, SiteItemComponent, NewSiteComponent
  ],
  //imports: [BrowserModule, HttpClientModule, AppRoutingModule],
  imports: [BrowserModule, HttpClientModule, ReactiveFormsModule, FormsModule, OrderModule, BrowserAnimationsModule, //AppRoutingModule
    RouterModule.forRoot([
      { path: "category", component: CategoryComponent, canActivate: [FirstGuard] },
      { path: "site", component: SiteComponent, canActivate: [FirstGuard] },
      { path: 'site/:categoryId', component: SiteComponent },
      { path: "query/:siteId", component: QueryComponent, canActivate: [FirstGuard] },
      { path: "position", component: PositionComponent, canActivate: [FirstGuard] },
      {
        path: "admin",
        //loadChildren: "app/admin/admin.module#AdminModule",
        loadChildren: () => import('src/app/admin/admin.module').then(m => m.AdminModule),
        canActivate: [FirstGuard]
      },
      { path: "**", redirectTo: "/category" }
  ])
],
  //providers: [CategoryRepository, RestDataSource],
  providers: [ RegionRepository, CategoryRepository, SiteRepository, QueryRepository, Summary, FirstGuard, Query, Site, Category,
    {provide: StaticDataSource, useClass: RestDataSource}
    //,{ provide: APP_INITIALIZER, useFactory: app_Init, deps: [SettingsHttpService], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
