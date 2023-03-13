import { AggregatedArticle } from './../../../../.api/nswag';
import { Component, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { BehaviorSubject, Observable } from 'rxjs';
import { ArticleService } from '../../services/article.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss'],
})
export class ArticleListComponent {
  public displayedColumns: string[] = [
    'articleId',
    'brand',
    'mainProductGroup',
    'productGroup',
  ];
  public dataSource = new MatTableDataSource([new AggregatedArticle()]);

  public _articleCount$: BehaviorSubject<number> = new BehaviorSubject(0);

  public _selectedArticle$: BehaviorSubject<AggregatedArticle> =
    new BehaviorSubject(new AggregatedArticle());

  public get articleCount$(): Observable<number> {
    return this._articleCount$.asObservable();
  }

  public get selectedArticle$(): Observable<AggregatedArticle> {
    return this._selectedArticle$.asObservable();
  }

  constructor(private _articleService: ArticleService) {
    this.refreshArticles();
  }

  @ViewChild(MatSort) sort: MatSort | undefined;

  public getArticleCount(): void {
    this._articleService.getArticleCount().subscribe((count) => {
      this._articleCount$.next(count);
    });
  }

  public refreshArticles(): void {
    this._articleService.getArticles().subscribe((articles) => {
      this.dataSource = new MatTableDataSource(articles);
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
      this._selectedArticle$.next(new AggregatedArticle());
    });
  }

  public onRowClicked(article: AggregatedArticle): void {
    this._selectedArticle$.next(article);
  }
}
