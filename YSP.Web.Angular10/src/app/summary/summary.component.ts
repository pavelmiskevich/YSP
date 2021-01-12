import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Summary } from "../model/summary.model";

@Component({
  selector: 'cart-summary',
  templateUrl: './summary.component.html'
  //,styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {

  constructor(private cd: ChangeDetectorRef, public summary: Summary) { }

  // ngAfterViewInit() {
  //   this.cd.detectChanges();
  // }

  ngOnInit(): void {
  }

}
