//https://habr.com/ru/company/infopulse/blog/358860/
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-todos',
  template: `
     <div *ngFor="let todo of todos">
       {{todo.title}} - {{runChangeDetection}}
     </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TodosComponent {
  @Input() todos;

  get runChangeDetection() {
    console.log('TodosComponent - Checking the view');
    return true;
  }
}