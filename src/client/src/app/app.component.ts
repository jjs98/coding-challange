import { BehaviorSubject, Observable } from 'rxjs';
import { ArticleService } from './services/article.service';
import { Component } from '@angular/core';
import { AggregatedArticle } from '../../.api/nswag';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  public title: string = 'Articlestore';
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
