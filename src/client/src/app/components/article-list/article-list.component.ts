import { Component } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AggregatedArticle } from '../../../../.api/nswag';
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent {
  public _articleCount$: BehaviorSubject<number> = new BehaviorSubject(0);
  public _articles$: BehaviorSubject<AggregatedArticle[]> = new BehaviorSubject([new AggregatedArticle()]);

  public get articleCount$(): Observable<number> {
    return this._articleCount$.asObservable();
  }

  public get articles$(): Observable<AggregatedArticle[]> {
    return this._articles$.asObservable();
  }

  constructor(private _articleService: ArticleService) {}

  public getArticleCount(): void {
    this._articleService.getArticleCount().subscribe((count) => {
      console.log(count);
      this._articleCount$.next(count);
    });
  }

  public getArticles(): void {
    this._articleService.getArticles().subscribe((articles) => {
      console.log(articles);
      this._articles$.next(articles);
    });
  }
}
