import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreditoRegisterComponent } from './credito-register.component';

describe('RegisterComponent', () => {
  let component: CreditoRegisterComponent;
  let fixture: ComponentFixture<CreditoRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreditoRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreditoRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
