import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthBarComponent } from './auth-bar.component';

describe('AuthBarComponent', () => {
  let component: AuthBarComponent;
  let fixture: ComponentFixture<AuthBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthBarComponent]
    });
    fixture = TestBed.createComponent(AuthBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
