import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailedMovieComponent } from './detailed-movie.component';

describe('DetailedMovieComponent', () => {
  let component: DetailedMovieComponent;
  let fixture: ComponentFixture<DetailedMovieComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DetailedMovieComponent]
    });
    fixture = TestBed.createComponent(DetailedMovieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
