import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteItemComponent } from './site-item.component';

describe('SiteItemComponent', () => {
  let component: SiteItemComponent;
  let fixture: ComponentFixture<SiteItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
