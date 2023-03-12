import { Observable } from 'rxjs';
import { AggregatedArticle, ArticlesClient } from '../../../.api/nswag';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  constructor(private _articleClient: ArticlesClient) {}

  public getArticles(): Observable<AggregatedArticle[]> {
    return this._articleClient.getArticles();
  }
  
  public getArticleCount(): Observable<number> {
    return this._articleClient.getArticlesCount();
  }
}
