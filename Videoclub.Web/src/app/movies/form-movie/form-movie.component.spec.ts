import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormMovieComponent } from './form-movie.component';

describe('EditMovieComponent', () => {
  let component: FormMovieComponent;
  let fixture: ComponentFixture<FormMovieComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FormMovieComponent]
    });
    fixture = TestBed.createComponent(FormMovieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
