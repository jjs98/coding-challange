import { Observable } from 'rxjs';
import { AggregatedArticle, ArticlesClient } from './../../../.api/nswag';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ArticleServiceService {
  constructor(private _articleClient: ArticlesClient) {}

  public getArticles(): Observable<AggregatedArticle[]> {
    return this._articleClient.getArticles();
  }
}
