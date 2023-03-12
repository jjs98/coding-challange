import { HttpClientModule } from '@angular/common/http';
import { ArticlesClient } from './../../.api/nswag';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { API_BASE_URL } from '../../.api/nswag';
import { environment } from '../../environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ArticleListComponent } from './components/article-list/article-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ArticleListComponent
  ],
  imports: [
    BrowserModule, 
    AppRoutingModule, 
    HttpClientModule
  ],
  providers: [
    ArticlesClient,
    {
      provide: API_BASE_URL,
      useValue: environment.API_BASE_URL,
    },
  ],
  bootstrap: [
    AppComponent
  ],
})
export class AppModule {}
