import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-queries',
  template: `
    <app-query [query]="query" *ngFor="let query of queries"></app-query>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class QueryListComponent {
  @Input() queries;
}