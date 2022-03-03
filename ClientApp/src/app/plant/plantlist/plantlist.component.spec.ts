import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlantlistComponent } from './plantlist.component';

describe('PlantlistComponent', () => {
  let component: PlantlistComponent;
  let fixture: ComponentFixture<PlantlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlantlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlantlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
