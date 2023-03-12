import { AggregatedAttribute } from './../../../../.api/nswag';
import { Component, Input } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AggregatedArticle } from '../../../../.api/nswag';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.scss'],
})
export class ArticleDetailsComponent {
  @Input() article$: Observable<AggregatedArticle> = of(
    new AggregatedArticle()
  );
  public selected: string = 'de';

  public getValueFromAttributes(attributes: AggregatedAttribute[] | undefined): string {
    let attribute = attributes?.find((x) => x.language == this.selected);
    return attribute?.value || '';
  }
}
