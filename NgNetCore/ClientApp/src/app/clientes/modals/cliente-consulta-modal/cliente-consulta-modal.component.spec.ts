import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteConsultaModalComponent } from './cliente-consulta-modal.component';

describe('ClienteConsultaModalComponent', () => {
  let component: ClienteConsultaModalComponent;
  let fixture: ComponentFixture<ClienteConsultaModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClienteConsultaModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClienteConsultaModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
